namespace Week5Proj;

public class Weapon
{
     public readonly string Name;
     public int MinDamage;
     public int MaxDamage;
     public bool IsTwoHanded;
     public int BonusToHit;

     public Weapon(string name)
     {
          var rand = new Random();
          Name = name;
          BonusToHit = rand.Next(1, 5);
     }

     public override string ToString()
     {
          return Name;
     }
}