namespace CookingProject.Api.Models;

public class RecipeStep
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int StepOrder { get; set; }
    public string Text { get; set; } = string.Empty;

    public Recipe? Recipe { get; set; }
}
