using RecipeApi.Models;

namespace RecipeApi.Data;

public class RecipeRepository
{
    private readonly List<Recipe> _recipes =
        [
            new Recipe { Id = 1, Title = "Spaghetti Bolognese", Description = "Italian pasta with rich tomato meat sauce", Ingredients = new List<string>{ "Pasta", "Meat", "Tomato Sauce" }, Instructions = "Cook pasta. Cook meat with tomato sauce. Mix together." },
            new Recipe { Id = 2, Title = "Chicken Curry", Description = "Spicy chicken curry with rice", Ingredients = ["Chicken", "Curry Paste", "Coconut Milk"], Instructions = "Cook chicken. Add curry paste and coconut milk. Serve with rice." },
            new Recipe { Id = 3, Title = "Pancakes", Description = "Fluffy pancakes for breakfast", Ingredients = ["Flour", "Eggs", "Milk"], Instructions = "Mix ingredients. Cook on a hot pan." },
            new Recipe { Id = 4, Title = "Caesar Salad", Description = "Classic salad with Caesar dressing", Ingredients = ["Lettuce", "Croutons", "Caesar Dressing"], Instructions = "Mix all ingredients in a bowl." },
            new Recipe { Id = 5, Title = "Beef Stew", Description = "Slow-cooked beef stew with vegetables", Ingredients = new List<string>{ "Beef", "Potatoes", "Carrots", "Onions" }, Instructions = "Slow-cook beef with vegetables." }
        ];

    public List<Recipe> GetAll() => _recipes;

    public Recipe? GetById(int id) => _recipes.FirstOrDefault(r => r.Id == id);

    public void Add(Recipe recipe)
    {
        recipe.Id = _recipes.Max(r => r.Id) + 1;
        _recipes.Add(recipe);
    }

    public void Update(Recipe recipe)
    {
        var existing = GetById(recipe.Id);
        if (existing != null)
        {
            existing.Title = recipe.Title;
            existing.Description = recipe.Description;
            existing.Ingredients = recipe.Ingredients;
            existing.Instructions = recipe.Instructions;
        }
    }

    public void Delete(int id)
    {
        var recipe = GetById(id);
        if (recipe != null)
        {
            _recipes.Remove(recipe);
        }
    }
}