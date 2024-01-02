using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;
using HeroWorld.HeroController;
using HeroWorld.ClassHeroController;
using HeroWorld.UserController;
using CloudinaryDotNet.Services;

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

// Connect EF Core with the DataBase
var connectionString = builder.Configuration.GetConnectionString("HeroWorld") ?? "Data Source = HeroWorld.db";
builder.Services.AddSqlite<HeroWorldDB>(connectionString);

var app = builder.Build();
// App Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroWorld API V1");
});

// app.MapGet("/cloudinary", async () =>
// {
//     var listResourcesByPrefixParams = new ListResourcesByPrefixParams()
//     {
//         Type = "upload"
//     };

//     var result = await cloudinary.ListResourcesAsync();

//     return result;
// });

app.MapPost("/cloudinary", async (InputCloudinary inputCloudinary) =>
{
    var result = await CloudinaryServices.Upload(inputCloudinary.Image);
    return result;
});

app.MapDelete("/cloudinary/{publicId}", async (string publicId) =>
{
    var result = await CloudinaryServices.Delete(publicId);
    return result;
});

app.MapGet("/users", UserController.GetAll);
app.MapPost("/users", UserController.Post);
app.MapGet("/users/{id}", UserController.Get);
app.MapPut("/users/{id}", UserController.Put);
app.MapDelete("/users/{id}", UserController.Delete);

app.MapGet("/heroes", HeroController.GetAll);
app.MapPost("/heroes", HeroController.Post);
app.MapGet("/heroes/{id}", HeroController.Get);
app.MapPut("/heroes/{id}", HeroController.Put);
app.MapDelete("/heroes/{id}", HeroController.Delete);

app.MapGet("/classes", ClassHeroController.GetAll);
app.MapPost("/classes", ClassHeroController.Post);
app.MapGet("/classes/{id}", ClassHeroController.Get);
app.MapPut("/classes/{id}", ClassHeroController.Put);
app.MapDelete("/classes/{id}", ClassHeroController.Delete);

app.Run();
