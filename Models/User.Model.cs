using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hero.Models;

namespace User.Models;


public class InputUser
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class UserModel : InputUser
{
    [Key]
    public int Id { get; set; }
    public ICollection<HeroModel> Heroes { get; set; } = new List<HeroModel>();
}

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<HeroDTO> Heroes { get; set; } = null!;
}

public class UserParse
{
    public static UserModel InputUserToUserModel(InputUser inputUser)
    {
        return new()
        {
            Id = 0,
            UserName = inputUser.UserName,
            Name = inputUser.Name,
            Email = inputUser.Email,
            Password = inputUser.Password,
            Heroes = new List<HeroModel>()
        };
    }
}