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
            if (playerCharacter.IsDead)
            {
                Console.WriteLine("You are dead. Game over!");
                break;
            }
            
            if (currentMonster.IsDead)
            {
                Console.WriteLine("Victory! Moving to the next room...");
                currentMonster = dungeon.GetMonster();
                currentRoom = dungeon.GetRoom();
                Console.WriteLine(currentRoom.Description);
                currentMonster.Info();
            }
            
            action = Dungeon.ShowMenu();
            switch (action)
            {
                case Actions.Invalid:
                    Console.WriteLine("I don't know what you mean.");
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
                    Console.WriteLine("Run away!");
                    currentMonster = dungeon.GetMonster();
                    currentRoom = dungeon.GetRoom();
                    Console.WriteLine(currentRoom.Description);
                    currentMonster.Info();
                    break;
                case Actions.Block:
                    Console.WriteLine(playerCharacter.CanBlock
                        ? "You prepare to block the next attack!"
                        : "You cannot block!");
                    break;
                case Actions.Exit:
                    break;
                case Actions.DoNothing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        } while (action != Actions.Exit);
    }
}