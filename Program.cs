using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;

var builder = WebApplication.CreateBuilder(args);

// Builder Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HeroWorld API",
        Description = "Making the Pizzas you love",
        Version = "V1"
    });
});
builder.Services.AddDbContext<HeroWorldDB>(options => options.UseInMemoryDatabase("items"));

var app = builder.Build();
// App Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroWorld API V1");
});

app.MapGet("/heroes", async (HeroWorldDB db) =>
{
    return await db.Heroes.ToListAsync();
});
app.MapPost("/heroes", async (HeroWorldDB db, Hero hero) =>
{
    await db.Heroes.AddAsync(hero);
    await db.SaveChangesAsync();
    return Results.Created($"/heroes/{hero.Id}", hero);
});
app.MapGet("/heroes/{id}", async (HeroWorldDB db, int id) => {
    return await db.Heroes.FindAsync(id);
});
app.MapPut("/heroes/{id}", async (HeroWorldDB db, Hero upHero, int id) =>
{
    Hero? hero = await db.Heroes.FindAsync(id);
    
    if(hero is null) return Results.NotFound();
    
    hero.Name = upHero.Name;
    hero.Description = upHero.Description;
    db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/heroes/{id}", async (HeroWorldDB db, int id) => {
    Hero? hero = await db.Heroes.FindAsync(id);
    if(hero is null) return Results.NotFound();
    db.Heroes.Remove(hero);
    db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
