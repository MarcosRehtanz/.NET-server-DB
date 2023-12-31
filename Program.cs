using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;
using HeroWorld.HeroController;
using HeroWorld.ClassController;

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
// JsonIgnore
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();
// App Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeroWorld API V1");
});

app.MapGet("/hero", HeroController.GetAll);
app.MapPost("/hero", HeroController.Post);
app.MapGet("/hero/{id}", HeroController.Get);
app.MapPut("/hero/{id}", HeroController.Put);
app.MapDelete("/hero/{id}", HeroController.Delete);

app.MapGet("/heroe/class", ClassController.GetAll);
app.MapPost("/heroe/class", ClassController.Post);
app.MapGet("/heroe/class/{id}", ClassController.Get);
app.MapPut("/heroe/class/{id}", ClassController.Put);
app.MapDelete("/heroe/class/{id}", ClassController.Delete);

app.Run();
