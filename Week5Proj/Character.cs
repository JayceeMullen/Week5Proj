namespace Week5Proj;

public class Character
{
    protected readonly Random Rand = new();
    protected Weapon EquippedWpn;
    protected int HitPoints { get; set; }
    private int MaxHitPoints { get; set; }
    protected int Level { get; private set; }
    
    public readonly string Name;
    public bool IsDead => HitPoints <= 0;

    public Character(string name, Weapon equippedWpn)
    {
        Name = name;
        EquippedWpn = equippedWpn;
        int hp = Rand.Next(10, 50);
        HitPoints = hp;
        MaxHitPoints = hp;
        Level = 1;
    }

    public int Attack()
    {
        int damage = Rand.Next(EquippedWpn.MinDamage, EquippedWpn.MaxDamage + 1) + EquippedWpn.MagicBonus + Level;
        Console.WriteLine($"{Name} swings their {EquippedWpn.Name} and " +
                          $"deals {damage} damage!");
        return damage;
    }

    public void Info()
    {
        Console.WriteLine($"{Name} has {HitPoints} hit points and is wielding a {EquippedWpn.Name} " +
                          $"at +{EquippedWpn.MagicBonus} to hit");
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        Console.WriteLine($"{Name} has {HitPoints} HP remaining.");
    }

    public void LevelUp()
    {
        Level++;
        MaxHitPoints += Rand.Next(1, 6);
        Console.WriteLine($"You gained a level! You are now Level {Level}! You now have {MaxHitPoints} HP!");
        Heal();
    }

    private void Heal()
    {
        HitPoints += MaxHitPoints / 2;
    }

}