namespace Week5Proj;

public class Weapon
{
     public readonly string Name;
     public int MinDamage;
     public int MaxDamage;
     public bool IsTwoHanded;
     public int BonusToHit;

     public Weapon(string name, int minDamage, int maxDamage, bool isTwoHanded, int bonusToHit)
     {
          Name = name;
          MinDamage = minDamage;
          IsTwoHanded = isTwoHanded;
          BonusToHit = bonusToHit;
          MaxDamage = maxDamage;
     }

     public Weapon(string name)
     {
          Name = name;
          MinDamage = 1;
     }

     public override string ToString()
     {
          return Name;
     }
}