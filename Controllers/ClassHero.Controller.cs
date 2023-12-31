using Microsoft.EntityFrameworkCore;
using HeroWorld.Models;
using ClassHero.Models;

namespace HeroWorld.ClassHeroController;

public class ClassHeroController
{
    public static async Task<List<ClassHeroModel>> GetAll(HeroWorldDB db)
    {
        return await db.Classes.Include(c=>c.Heroes).ToListAsync();
    }
    public static async Task<IResult> Post(HeroWorldDB db, InputClassHero inputClassHero)
    {
        ClassHeroModel classHero = (ClassHeroModel)inputClassHero;
        await db.Classes.AddAsync(classHero);
        var _ = db.SaveChangesAsync();
        return Results.Created($"/hero/class/{classHero.Id}", classHero);
    }
    public static async Task<ClassHeroModel?> Get(HeroWorldDB db, int id)
    {
        ClassHeroModel? hClass = await db.Classes.FindAsync(id);
        if (hClass is null) return hClass;
        await db.Entry(hClass)
            .Collection(hc => hc.Heroes)
            .LoadAsync();
        return hClass;
    }
    public static async Task<IResult> Put(HeroWorldDB db, InputClassHero upClassHero, int id)
    {
        ClassHeroModel? heroClass = await db.Classes.FindAsync(id);

        if (heroClass is null) return Results.NotFound();

        heroClass.Name = upClassHero.Name;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    public static async Task<IResult> Delete(HeroWorldDB db, int id)
    {
        ClassHeroModel? heroClass = await db.Classes.FindAsync(id);
        if (heroClass is null) return Results.NotFound();
        db.Classes.Remove(heroClass);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}