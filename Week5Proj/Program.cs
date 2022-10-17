using Bogus;

namespace Week5Proj;

public static class Program
{
    public static void Main()
    {
        //Set Randomizer and variables for Bogus
        Randomizer.Seed = new Random(0451);

        var dungeon = new Dungeon("Test Title", 10, 10);
        var playerCharacter = new Character(WeaponsHelper.GetWeapon());
        
        playerCharacter.Info();
        Character currentMonster = dungeon.GetMonster();
        Room currentRoom = dungeon.GetRoom();
        
        Console.WriteLine($"Welcome to: {dungeon.Title}!");
        Console.WriteLine(currentRoom.Description);
        currentMonster.Info();
        Actions action;
        do
        { 
            if (currentMonster.IsDead)
            {
                Console.WriteLine("Victory! Moving to the next room...");
                currentMonster = dungeon.GetMonster();
                currentRoom = dungeon.GetRoom();
                Console.WriteLine(currentRoom.Description);
                currentMonster.Info();
            }

            if (playerCharacter.IsDead)
            {
                Console.WriteLine("You are dead. Game over!");
                break;
            }
            action = Dungeon.ShowMenu();
            switch (action)
            {
                case Actions.Invalid:
                    Console.WriteLine("That is an invalid action.");
                    continue;
                case Actions.Attack:
                    currentMonster.TakeDamage(playerCharacter.Attack());
                    break;
                case Actions.PlayerInfo:
                    playerCharacter.Info();
                    break;
                case Actions.MonsterInfo:
                    currentMonster.Info();
                    break;
                case Actions.RunAway:
                    playerCharacter.RunAway();
                    break;
            }
        } while (action != Actions.Exit);
    }
}