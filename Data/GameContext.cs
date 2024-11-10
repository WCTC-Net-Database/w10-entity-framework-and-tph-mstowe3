using System.Configuration.Assemblies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using W9_assignment_template.Models;
using W9_assignment_template.Models.Abilities;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ability> Abilities {get;set;}

    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure TPH for Character hierarchy
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("CharacterType")
            .HasValue<Player>("Player")
            .HasValue<Goblin>("Goblin");

        base.OnModelCreating(modelBuilder);

        // Configure TPH for Ability Hierarchy
        modelBuilder.Entity<Ability>()
        .HasDiscriminator<string>("Name")
        .HasValue<WalkAbility>("Walk")
        .HasValue<StabAbility>("Stab")
        .HasValue<MagicAbility>("Magic Attack");

        // Configure many-to-many relationship between Character and Ability
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Abilities)
            .WithMany(a => a.Characters)
            .UsingEntity(j => j.ToTable("CharacterAbilities"));

        base.OnModelCreating(modelBuilder);
    }

public void Seed()
{
    try
    {
        var room1 = new Room { Name = "Entrance Hall", Description = "The main entry." };
        var room2 = new Room { Name = "Treasure Room", Description = "A room filled with treasures." };

        var character1 = new Player { Name = "Jane", Level = 1, Room = room1 };
        var character2 = new Goblin { Name = "JoJo", Level = 2, Room = room2 };

        Rooms.AddRange(room1, room2);
        Characters.AddRange(character1, character2);

        var walk = new WalkAbility { Description = "The Character Can Walk." };
        var stab = new StabAbility { Description = "The Character Can Stab." };
        var magic = new MagicAbility { Description = "The Character Can Attack with Magic." };

        Abilities.AddRange(walk, stab, magic);

        SaveChanges();
    }
    catch (Exception ex)
    {
        
        Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
        Console.WriteLine(ex.StackTrace); 
    }
}


}
