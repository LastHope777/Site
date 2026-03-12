using CookingProject.Api.Data;
using CookingProject.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public CommentsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> Get([FromQuery] int recipeId)
    {
        var comments = await _db.Comments
            .Where(c => c.RecipeId == recipeId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new
            {
                c.Id,
                c.Author,
                c.Text,
                CreatedAt = c.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
            })
            .ToListAsync();

        return Ok(comments);
    }

    public record CreateCommentRequest(int RecipeId, string Author, string Text);

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCommentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Author) || string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { success = false, message = "Author and text are required" });
        }

        var comment = new Comment
        {
            RecipeId = request.RecipeId,
            Author = request.Author,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return Ok(new { success = true, id = comment.Id });
    }
}

