namespace W9_assignment_template.Models;

public abstract class Character : ICharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }

    // Foreign key to Room
    public int RoomId { get; set; }

    // Navigation property to Room
    public virtual Room Room { get; set; }
    public virtual ICollection<Ability>Abilities {get;set;}
    public virtual void Attack(ICharacter target)
    {
        Console.WriteLine($"{Name} attacks {target.Name}!");
    }

    public virtual void ExecuteAbility(Ability ability)
    {
        Console.WriteLine($"{Name} uses {ability.Description}!");
    }
}