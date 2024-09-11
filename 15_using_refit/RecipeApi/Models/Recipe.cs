namespace RecipeApi.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public string Instructions { get; set; }
}