namespace CookingProject.Api.Models;

public class CreateIngredientRequest
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public string? Unit { get; set; }
    public int BasePortions { get; set; }
    public bool IsFlexible { get; set; }
}
