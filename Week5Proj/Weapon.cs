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

     public override string ToString()
     {
          return Name;
     }
}