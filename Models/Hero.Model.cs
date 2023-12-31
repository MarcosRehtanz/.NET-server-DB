using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ClassHero.Models;

namespace Hero.Models;


public class InputHero
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int ClassId { get; set; }
}

public class HeroModel : InputHero
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("ClassId"), JsonIgnore]
    public ClassHeroModel Class { get; set; } = null!;
}

public class HeroDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ClassDTO Class { get; set; } = null!;
}