using ClassHero.Models;
using Hero.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroWorld.Models;

public class HeroWorldDB(DbContextOptions options) : DbContext(options)
{
    public DbSet<HeroModel> Heroes { get; set; } = null!;
    public DbSet<ClassHeroModel> Classes { get; set; } = null!;
}