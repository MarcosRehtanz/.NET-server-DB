# .NET-server-DB

## Config

### API web
```CLI de .NET
dotnet new web -o <name-project> -f net8.0
```

### Swashbuckle
#### Install
```CLI de .NET
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
```
#### Config
```C#
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World!");

app.Run();
```

### Create Models
>  Models <br>
>  |→ DataBase.Model.cs
```C#
namespace DataBase.Model.cs;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Surname { get; set; }
}
```
### EntityFrameworkCore (EF Core)
#### InMemory
- ```dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0```
##### Config 
>  → Program.cs
```C#
using Microsoft.Entity.FrameworkCore;

builder.Services.AddDbContext<AplicationDb>(options => options.UseInMemoryDatabase("items"));
```
<br>

#### SQLite
- Install
    ```
    > dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0
    
    > dotnet tool install --global dotnet-ef
    
    > dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0
    ```
- Enable to creating of DB
    - >  → Program.cs
        ```C#
        using Microsoft.Entity.FrameworkCore;

        var connectionString = builder.Configuration.GetConnectionString("Aplication") ?? "Data Source=Aplication.db";

        builder.Services.AddSqlite<AplicationDb>(connectionString);
        ```
    -   ```CLI de .NET
        > dotnet ef migrations add InitialCreate
        
        > dotnet ef database update
        ```
<br>

#### DataBaseContext
>  Models <br>
>  |→ DataBase.Model.cs
```c#
using Microsoft.Entity.FrameworkCore;

public class AplicationDb : DbContext
{
    public AplicationDB(DbContextOption options)
        : base(options) { }
    public DbSet<User> Users { get; set; } = null!;
}
```