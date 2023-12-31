using Microsoft.EntityFrameworkCore;
using User.Models;
using Hero.Models;
using ClassHero.Models;

namespace HeroWorld.Models;

public class HeroWorldDB(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<HeroModel> Heroes { get; set; } = null!;
    public DbSet<ClassHeroModel> Classes { get; set; } = null!;
}