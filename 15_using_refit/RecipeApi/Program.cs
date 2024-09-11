using RecipeApi.Models;
using RecipeApi.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var repository = new RecipeRepository();

app.MapGet("/api/recipe", () => repository.GetAll());

app.MapGet("/api/recipe/{id:int}", (int id) =>
{
    var recipe = repository.GetById(id);
    return recipe is not null ? Results.Ok(recipe) : Results.NotFound();
});

app.MapPost("/api/recipe", (Recipe recipe) =>
{
    repository.Add(recipe);
    return Results.Created($"/api/recipe/{recipe.Id}", recipe);
});

app.MapPut("/api/recipe/{id:int}", (int id, Recipe updatedRecipe) =>
{
    if (id != updatedRecipe.Id)
        return Results.BadRequest();

    var existingRecipe = repository.GetById(id);
    if (existingRecipe is null)
        return Results.NotFound();

    repository.Update(updatedRecipe);
    return Results.NoContent();
});

app.MapDelete("/api/recipe/{id:int}", (int id) =>
{
    var existingRecipe = repository.GetById(id);
    if (existingRecipe is null)
        return Results.NotFound();

    repository.Delete(id);
    return Results.NoContent();
});

app.UseHttpsRedirection();

app.Run();