namespace CookingProject.Api.Models;

public class Comment
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Recipe? Recipe { get; set; }
}

