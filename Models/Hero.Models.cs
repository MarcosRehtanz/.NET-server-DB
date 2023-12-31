using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HeroWorld.Models;

public class HeroWorldDB(DbContextOptions options) : DbContext(options)
{
    public DbSet<Hero> Heroes { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
}

public class Hero
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public int ClassId { get; set; }
    [ForeignKey("ClassId"), JsonIgnore]
    public Class Class { get; set; } = null!;
}

public class Class
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public ICollection<Hero> Heroes { get; set; } = new List<Hero>();
}

public class HeroDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ClassDTO Class { get; set; } = null!;
}
public class ClassDTO
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}