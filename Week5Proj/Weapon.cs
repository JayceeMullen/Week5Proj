namespace Week5Proj;

public class Weapon
{
     public string Name { get; init; } = null!;
     public int MinDamage { get; set; }
     public int MaxDamage { get; set; }
     public bool IsTwoHanded { get; set; }
     public int MagicBonus { get; init; }

     public Weapon(string name)
     {
          Name = name;
          MagicBonus = 0;
     }

     public Weapon()
     {
     }

     public Weapon(Weapon baseWeapon, int level)
     {
          var rand = new Random();
          int bonus = rand.Next(0, level);

          Name = bonus == 0 ? baseWeapon.Name : $"{baseWeapon.Name} +{bonus}";
          MinDamage = baseWeapon.MinDamage;
          MaxDamage = baseWeapon.MaxDamage;
          IsTwoHanded = baseWeapon.IsTwoHanded;
          MagicBonus = bonus;
     }

     public override string ToString()
     {
          return Name;
     }
}