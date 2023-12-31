using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;

namespace HeroWorld.HeroController;

public class HeroController
{
    public static async Task<List<HeroDTO>> GetAll(HeroWorldDB db)
    {
        return await db.Heroes
            .Select(h => new HeroDTO
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                Class = new ClassDTO
                {
                    Id = h.ClassId,
                    Name = h.Class.Name
                }
            })
            .ToListAsync();
    }
    public static async Task<IResult> Post(HeroWorldDB db, Hero hero)
    {
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
                Class = new ClassDTO
                {
                    Id = h.Class.Id,
                    Name = h.Class.Name
                }
            })
            .FirstOrDefaultAsync();
    }
    public static async Task<IResult> Put(HeroWorldDB db, Hero upHero, int id)
    {
        Hero? hero = await db.Heroes.FindAsync(id);

        if (hero is null) return Results.NotFound();

        hero.Name = upHero.Name;
        hero.Description = upHero.Description;
        hero.ClassId = upHero.ClassId;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    public static async Task<IResult> Delete(HeroWorldDB db, int id)
    {
        Hero? hero = await db.Heroes.FindAsync(id);
        if (hero is null) return Results.NotFound();
        db.Heroes.Remove(hero);
        var _ = db.SaveChangesAsync();
        return Results.Ok();
    }
}