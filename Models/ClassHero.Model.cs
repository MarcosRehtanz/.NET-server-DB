using System.ComponentModel.DataAnnotations;
using Hero.Models;

namespace ClassHero.Models;

public class InputClassHero
{
    [Required]
    public string Name { get; set; } = null!;
}

public class ClassHeroModel : InputClassHero
{
    [Key]
    public int Id { get; set; }
    public ICollection<HeroModel> Heroes { get; set; } = new List<HeroModel>();
}

public class ClassDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}