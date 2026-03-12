namespace CookingProject.Api.Models;

public class CreateRecipeRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Cuisine { get; set; }
    public string? DishType { get; set; }
    public string? MainIngredient { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsLactoseFree { get; set; }
    public bool IsGlutenFree { get; set; }
    public bool IsSugarFree { get; set; }
    public List<CreateIngredientRequest>? Ingredients { get; set; }
    public List<string>? Steps { get; set; }
}
