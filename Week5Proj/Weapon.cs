namespace Week5Proj;

public class Weapon
{
     public readonly string Name;
     public int MinDamage;
     public int MaxDamage;
     public bool IsTwoHanded;
     public readonly int MagicBonus;

     public Weapon(string name)
     {
          Name = name;
          MagicBonus = 0;
     }

     public override string ToString()
     {
          return Name;
     }
}