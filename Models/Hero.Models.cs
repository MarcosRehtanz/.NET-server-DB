using Microsoft.EntityFrameworkCore;

namespace HeroWorld.Models;

public class HeroWorldDB(DbContextOptions options) : DbContext(options)
{
    public DbSet<Hero> Heroes { get; set; } = null!;
}

public class Hero
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}