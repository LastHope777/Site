using CookingProject.Api.Data;
using CookingProject.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private const string DemoUserId = "demo-user";

    public FavoritesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<int>>> Get()
    {
        var ids = await _db.Favorites
            .Where(f => f.UserId == DemoUserId)
            .Select(f => f.RecipeId)
            .ToListAsync();

        return Ok(ids);
    }

    public record FavoriteRequest(int RecipeId, string Action);

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] FavoriteRequest request)
    {
        var existing = await _db.Favorites
            .FirstOrDefaultAsync(f => f.UserId == DemoUserId && f.RecipeId == request.RecipeId);

        var action = request.Action?.ToLower() ?? "add";
        bool isFavorite;

        if (action == "add")
        {
            if (existing == null)
            {
                _db.Favorites.Add(new Favorite
                {
                    UserId = DemoUserId,
                    RecipeId = request.RecipeId
                });
                await _db.SaveChangesAsync();
            }
            isFavorite = true;
        }
        else if (action == "remove")
        {
            if (existing != null)
            {
                _db.Favorites.Remove(existing);
                await _db.SaveChangesAsync();
            }
            isFavorite = false;
        }
        else
        {
            return BadRequest(new { success = false, message = "Unknown action" });
        }

        return Ok(new { success = true, isFavorite });
    }
}

