namespace CookingProject.Api.Models;

public class Favorite
{
    public int Id { get; set; }

    // Для упрощения считаем, что у нас один пользователь.
    public string UserId { get; set; } = "demo-user";

    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}

