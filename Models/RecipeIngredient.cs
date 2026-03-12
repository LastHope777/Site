namespace CookingProject.Api.Models;

public class RecipeIngredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int BasePortions { get; set; } = 4;      

    public bool IsFlexible { get; set; }

    public Recipe? Recipe { get; set; }
}
