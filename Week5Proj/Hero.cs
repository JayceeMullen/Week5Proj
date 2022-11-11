namespace Week5Proj;

public class Hero : Character
{
    private bool _isWieldingShield;
    public bool CanBlock => _isWieldingShield && !EquippedWpn.IsTwoHanded;
    
    public bool IsBlocking { get; set; }

    public Hero(string pcName, Weapon equippedWpn, bool wantsShield) : base(pcName, equippedWpn)
    {
        if (!wantsShield) return;
        
        while (EquippedWpn.IsTwoHanded)
        {
            EquippedWpn = WeaponsHelper.GetWeapon();
        }

        _isWieldingShield = true;
    }

    public new int Attack()
    {
        int damage = Rand.Next(EquippedWpn.MinDamage, EquippedWpn.MaxDamage + 1) + EquippedWpn.MagicBonus + Level;
        damage = IsBlocking ? damage / 2 : damage;
        Console.WriteLine($"{Name} swings their {EquippedWpn.Name} and " +
                          $"deals {damage} damage!");
        return damage;
    }

    public new void TakeDamage(int damage)
    {
        HitPoints -= IsBlocking ? damage / 2 : damage;
        Console.WriteLine($"{Name} has {HitPoints} HP remaining.");
    }
    
    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon.IsTwoHanded && _isWieldingShield)
        {
            Console.WriteLine("You cannot wield this weapon and a shield. Would you like to drop your shield?... Y/N");
            string? selection = Console.ReadLine();
            if (selection != null && selection.ToLower() == "y")
            {
                _isWieldingShield = false;
            }
        }
        
        EquippedWpn = newWeapon;
    }
}