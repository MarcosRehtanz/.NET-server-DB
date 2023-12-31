using ClassHero.Models;
using Hero.Models;
using HeroWorld.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace HeroWorld.UserController;

public class UserController
{
    public static async Task<List<UserDTO>> GetAll(HeroWorldDB db)
    {
        return await db.Users
            .Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Heroes = (ICollection<HeroDTO>)user.Heroes.Select( hero => new HeroDTO
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Description = hero.Description,
                    Class = new ClassDTO
                    {
                        Id = hero.Class.Id,
                        Name = hero.Class.Name
                    }
                })
            })
            .ToListAsync();
    }
    public static async Task<IResult> Post(HeroWorldDB db, InputUser inputUser)
    {
        UserModel user = UserParse.InputUserToUserModel(inputUser);
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return Results.Created($"/users/{user.Id}", user);
    }
    public static async Task<UserModel?> Get(HeroWorldDB db, int id)
    {
        return await db.Users
            .Where(User => User.Id == id)
            .FirstOrDefaultAsync();
    }
    public static async Task<IResult> Put(HeroWorldDB db, InputUser upUser, int id)
    {
        UserModel? user = await db.Users.FindAsync(id);

        if (user is null) return Results.NotFound();

        user.Name = upUser.Name;
        user.UserName = upUser.UserName;
        user.Email = upUser.Email;
        user.Password = upUser.Password;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    public static async Task<IResult> Delete(HeroWorldDB db, int id)
    {
        UserModel? user = await db.Users.FindAsync(id);
        if (user is null) return Results.NotFound();
        db.Users.Remove(user);
        var _ = db.SaveChangesAsync();
        return Results.Ok();
    }
}