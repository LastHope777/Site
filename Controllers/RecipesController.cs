using CookingProject.Api.Data;
using CookingProject.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public RecipesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> Get([FromQuery] string? tags, [FromQuery] string? q)
    {
        IQueryable<Recipe> query = _db.Recipes;

        if (!string.IsNullOrWhiteSpace(tags))
        {
            var tagList = tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                              .Select(t => t.ToLower())
                              .ToList();

            foreach (var tag in tagList)
            {
                query = query.Where(r =>
                    r.Cuisine.ToLower() == tag ||
                    r.DishType.ToLower() == tag ||
                    r.MainIngredient.ToLower() == tag ||
                    (tag == "veg" && r.IsVegetarian) ||
                    (tag == "veggy" && r.MainIngredient.ToLower() == "veggy") ||
                    (tag == "nolact" && r.IsLactoseFree) ||
                    (tag == "nogluten" && r.IsGlutenFree) ||
                    (tag == "nosugar" && r.IsSugarFree)
                );
            }
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            var search = q.ToLower();
            query = query.Where(r => r.Title.ToLower().Contains(search) ||
                                     r.Description.ToLower().Contains(search));
        }

        var result = await query
            .OrderBy(r => r.Id)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Description,
                r.Image
            })
            .ToListAsync();

        return Ok(result);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<object>> GetById(int id, [FromQuery] int portions = 4)
    {
        var recipe = await _db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null)
            return NotFound();

        var scaledIngredients = recipe.Ingredients
            .OrderBy(i => i.Id)
            .Select(i =>
            {
                if (i.IsFlexible)
                    return new { i.Name, DisplayAmount = i.Unit };
                var factor = (decimal)portions / i.BasePortions;
                var scaled = Math.Round(i.Amount * factor, 1);
                var disp = scaled == (int)scaled ? ((int)scaled).ToString() : scaled.ToString("0.0");
                return new { i.Name, DisplayAmount = $"{disp} {i.Unit}" };
            })
            .ToList();

        var steps = recipe.Steps
            .OrderBy(s => s.StepOrder)
            .Select(s => s.Text)
            .ToList();

        return Ok(new
        {
            recipe.Id,
            recipe.Title,
            recipe.Description,
            recipe.Image,
            Portions = portions,
            Ingredients = scaledIngredients,
            Steps = steps
        });
    }


    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] CreateRecipeRequest request)
    {
        var errors = ValidateRecipeRequest(request);
        if (errors.Count > 0)
            return BadRequest(new { success = false, message = string.Join("; ", errors) });

        var recipe = new Recipe
        {
            Title = request.Title!.Trim(),
            Description = request.Description!.Trim(),
            Image = request.Image!.Trim(),
            Cuisine = request.Cuisine!.ToLower(),
            DishType = request.DishType!.ToLower(),
            MainIngredient = request.MainIngredient!.ToLower(),
            IsVegetarian = request.IsVegetarian,
            IsLactoseFree = request.IsLactoseFree,
            IsGlutenFree = request.IsGlutenFree,
            IsSugarFree = request.IsSugarFree
        };

        _db.Recipes.Add(recipe);
        await _db.SaveChangesAsync();

        foreach (var ing in request.Ingredients!)
        {
            _db.RecipeIngredients.Add(new RecipeIngredient
            {
                RecipeId = recipe.Id,
                Name = ing.Name.Trim(),
                Amount = ing.Amount,
                Unit = ing.Unit?.Trim() ?? "шт.",
                BasePortions = Math.Clamp(ing.BasePortions, 1, 20),
                IsFlexible = ing.IsFlexible
            });
        }

        int order = 1;
        foreach (var step in request.Steps!)
        {
            _db.RecipeSteps.Add(new RecipeStep
            {
                RecipeId = recipe.Id,
                StepOrder = order++,
                Text = step.Trim()
            });
        }

        await _db.SaveChangesAsync();

        return Ok(new { success = true, id = recipe.Id });
    }

    private static readonly HashSet<string> ValidCuisines = new(StringComparer.OrdinalIgnoreCase)
        { "russian", "italian", "japan", "china", "american", "french", "arabic" };
    private static readonly HashSet<string> ValidDishTypes = new(StringComparer.OrdinalIgnoreCase)
        { "soup", "salad", "dessert", "main", "garnish", "drink" };
    private static readonly HashSet<string> ValidMainIngredients = new(StringComparer.OrdinalIgnoreCase)
        { "meat", "veggy", "fish", "grain" };

    private static List<string> ValidateRecipeRequest(CreateRecipeRequest? r)
    {
        var errors = new List<string>();
        if (r == null)
        {
            errors.Add("Данные не переданы");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(r.Title))
            errors.Add("Название обязательно");
        else if (r.Title.Trim().Length < 3 || r.Title.Length > 200)
            errors.Add("Название: от 3 до 200 символов");

        if (string.IsNullOrWhiteSpace(r.Description))
            errors.Add("Описание обязательно");
        else if (r.Description.Trim().Length < 10 || r.Description.Length > 2000)
            errors.Add("Описание: от 10 до 2000 символов");

        if (string.IsNullOrWhiteSpace(r.Image))
            errors.Add("Изображение обязательно");
        else if (r.Image.Length > 100 || !System.Text.RegularExpressions.Regex.IsMatch(r.Image.Trim(), @"^[a-zA-Z0-9_\-\.]+\.(jpg|jpeg|png|gif)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            errors.Add("Изображение: корректное имя файла (например recipe.jpg)");

        if (string.IsNullOrWhiteSpace(r.Cuisine) || !ValidCuisines.Contains(r.Cuisine))
            errors.Add("Выберите допустимую кухню");
        if (string.IsNullOrWhiteSpace(r.DishType) || !ValidDishTypes.Contains(r.DishType))
            errors.Add("Выберите допустимый тип блюда");
        if (string.IsNullOrWhiteSpace(r.MainIngredient) || !ValidMainIngredients.Contains(r.MainIngredient))
            errors.Add("Выберите допустимый основной ингредиент");

        if (r.Ingredients == null || r.Ingredients.Count == 0)
            errors.Add("Добавьте минимум один ингредиент");
        else
        {
            for (int i = 0; i < r.Ingredients.Count; i++)
            {
                var ing = r.Ingredients[i];
                if (string.IsNullOrWhiteSpace(ing?.Name))
                    errors.Add($"Ингредиент {i + 1}: укажите название");
            }
        }

        if (r.Steps == null || r.Steps.Count == 0)
            errors.Add("Добавьте минимум один шаг приготовления");

        return errors;
    }
}

