using Refit;
using RecipeClient.ApiContract;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<IRecipeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5099"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/recipe/{id}", async (int id, IRecipeApi api) =>
    await api.GetRecipeAsync(id));

app.MapGet("/recipe", async (IRecipeApi api) =>
    await api.GetRecipesAsync());

app.MapPost("/recipe", async ([FromBody] Recipe recipe, IRecipeApi api) =>
    await api.CreateRecipeAsync(recipe));

app.MapPut("/recipe/{id}", async (int id, [FromBody] Recipe recipe, IRecipeApi api) =>
    await api.UpdateRecipeAsync(id, recipe));

app.MapDelete("/recipe/{id}", async (int id, IRecipeApi api) =>
    await api.DeleteRecipeAsync(id));

app.UseHttpsRedirection();

app.Run();