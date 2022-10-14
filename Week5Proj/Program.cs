using Bogus;

namespace Week5Proj;

public static class Program
{
    public static void Main()
    {
        //Set Randomizer and variables for Bogus
        Randomizer.Seed = new Random(0451);
        var rand = new Random();

        var weapons = new List<Weapon>(WeaponsHelper.GetWeapons());
        var dungeon = new Dungeon("Test Title", 10, 10);
        var playerCharacter = new Character(weapons[rand.Next(weapons.Count)]);

        foreach (Weapon weapon in weapons)
        {
            Console.WriteLine(weapon.ToString());
        }

        Console.WriteLine(playerCharacter.ToString());

        Actions action;
        do
        { 
            action = Dungeon.ShowMenu();
            
            
        } while (action != Actions.Exit);
    }
}