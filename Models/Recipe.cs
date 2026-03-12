namespace CookingProject.Api.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public string Cuisine { get; set; } = string.Empty;      // russian, italian, japan, china, american, french, arabic
    public string DishType { get; set; } = string.Empty;     // soup, garnish, drink, salad, dessert, main
    public string MainIngredient { get; set; } = string.Empty; // meat, veggy, veg, fish, grain

    public bool IsVegetarian { get; set; }
    public bool IsLactoseFree { get; set; }
    public bool IsGlutenFree { get; set; }
    public bool IsSugarFree { get; set; }

    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
}

