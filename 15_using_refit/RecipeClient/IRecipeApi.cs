using Refit;

namespace RecipeClient.ApiContract;

public interface IRecipeApi 
{
    [Get("/api/recipe/{id}")]
    Task<Recipe> GetRecipeAsync(int id);

    [Get("/api/recipe")]
    Task<List<Recipe>> GetRecipesAsync();

    [Post("/api/recipe")]
    Task<Recipe> CreateRecipeAsync([Body] Recipe recipe);

    [Put("/api/recipe/{id}")]
    Task<Recipe> UpdateRecipeAsync(int id, [Body] Recipe recipe);

    [Delete("/api/recipe/{id}")]
    Task DeleteRecipeAsync(int id);
}

public class Recipe
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public string? Instructions { get; set; }
}