using Bogus;

namespace Week5Proj;

public class Character
{
    private readonly Random _rand = new();
    private readonly string _name;
    private Weapon _equippedWpn;
    private int HitPoints { get; set; }
    private bool CanBlock => !_equippedWpn.IsTwoHanded;
    
    public bool IsDead => HitPoints <= 0;

    protected Character()
    {
        
    }

    public Character(Weapon equippedWpn)
    {
        var faker = new Faker();
        
        _name = faker.Name.FirstName();
        _equippedWpn = equippedWpn;
        HitPoints = _rand.Next(10, 50);
    }
    public Character(string name, Weapon equippedWpn)
    {
        _name = name;
        _equippedWpn = equippedWpn;
        HitPoints = _rand.Next(10, 50);
    }

    public int Attack()
    {
        int damage = _rand.Next(_equippedWpn.MinDamage, _equippedWpn.MaxDamage + 1);
        Console.WriteLine($"{_name} swings their {_equippedWpn.Name} and " +
                          $"deals {damage} damage!");
        return damage;
    }

    public void Info()
    {
        Console.WriteLine($"{_name} has {HitPoints} hit points and is wielding a {_equippedWpn.Name} " +
                          $"at +{_equippedWpn.BonusToHit} to hit");
    }

    public void RunAway()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        Console.WriteLine($"{_name} has {HitPoints} remaining.");
    }
}