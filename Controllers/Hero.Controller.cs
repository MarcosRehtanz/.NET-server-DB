using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;
using Hero.Models;

namespace HeroWorld.HeroController;

public class HeroController
{
    public static async Task<List<HeroDTO>> GetAll(HeroWorldDB db)
    {
        return await db.Heroes
                .Select(hero => new HeroDTO
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Description = hero.Description,
                    User = new()
                    {
                        Id = hero.User.Id,
                        Name = hero.User.Name
                    },
                    Class = new()
                    {
                        Id = hero.ClassId,
                        Name = hero.Class.Name
                    }
                })
                .ToListAsync();
    }
    public static async Task<IResult> Post(HeroWorldDB db, InputHero inputHero)
    {
        HeroModel hero = HeroParse.InputHeroToHeroModel(inputHero);
        await db.Heroes.AddAsync(hero);
        await db.SaveChangesAsync();
        return Results.Created($"/hero/{hero.Id}", hero);
    }
    public static async Task<HeroDTO?> Get(HeroWorldDB db, int id)
    {
        return await db.Heroes
            .Where(h => h.Id == id)
            .Select(h => new HeroDTO
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Class = new()
                {
                    Id = h.Class.Id,
                    Name = h.Class.Name
                }
            })
            .FirstOrDefaultAsync();
    }
    public static async Task<IResult> Put(HeroWorldDB db, InputHero inputHero, int id)
    {
        HeroModel? hero = await db.Heroes.FindAsync(id);

        if (hero is null) return Results.NotFound();

        hero.Name = inputHero.Name;
        hero.Description = inputHero.Description;
        hero.ClassId = inputHero.ClassId;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    public static async Task<IResult> Delete(HeroWorldDB db, int id)
    {
        HeroModel? hero = await db.Heroes.FindAsync(id);
        if (hero is null) return Results.NotFound();
        db.Heroes.Remove(hero);
        var _ = db.SaveChangesAsync();
        return Results.Ok();
    }
}