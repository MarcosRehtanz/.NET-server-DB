using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ClassHero.Models;
using User.Models;

namespace Hero.Models;


public class InputHero
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int UserId { get; set; }
    [Required]
    public int ClassId { get; set; }
}

public class HeroModel : InputHero
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("UserId"), JsonIgnore]
    public UserModel User { get; set; } = null!;
    [ForeignKey("ClassId"), JsonIgnore]
    public ClassHeroModel Class { get; set; } = null!;
}

public class HeroDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public UserDTO User { get; set; } = null!;
    public ClassDTO Class { get; set; } = null!;
}

public class HeroParse
{
    public static HeroModel InputHeroToHeroModel(InputHero inputHero)
    {
        return new()
        {
            Name = inputHero.Name,
            Description = inputHero.Description,
            UserId = inputHero.UserId,
            ClassId = inputHero.ClassId
        };
    }
}