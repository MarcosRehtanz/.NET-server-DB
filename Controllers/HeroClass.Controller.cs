using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;
namespace HeroWorld.ClassController;

public class ClassController
{
    public static async Task<List<Class>> GetAll(HeroWorldDB db)
    {
        return await db.Classes.Include(c=>c.Heroes).ToListAsync();
    }
    public static async Task<IResult> Post(HeroWorldDB db, Class heroClass)
    {
        await db.Classes.AddAsync(heroClass);
        var _ = db.SaveChangesAsync();
        return Results.Created($"/hero/class/{heroClass.Id}", heroClass);
    }
    public static async Task<Class?> Get(HeroWorldDB db, int id)
    {
        Class? hClass = await db.Classes.FindAsync(id);
        if (hClass is null) return hClass;
        await db.Entry(hClass)
            .Collection(hc => hc.Heroes)
            .LoadAsync();
        return hClass;
    }
    public static async Task<IResult> Put(HeroWorldDB db, Class upHeroClass, int id)
    {
        Class? heroClass = await db.Classes.FindAsync(id);

        if (heroClass is null) return Results.NotFound();

        heroClass.Name = upHeroClass.Name;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    public static async Task<IResult> Delete(HeroWorldDB db, int id)
    {
        Class? heroClass = await db.Classes.FindAsync(id);
        if (heroClass is null) return Results.NotFound();
        db.Classes.Remove(heroClass);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}