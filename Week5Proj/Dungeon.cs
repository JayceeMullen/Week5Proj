using Bogus;

namespace Week5Proj;

public class Dungeon
{
    private readonly Room[] _rooms;
    private readonly Character[] _monsters;

    private string Title { get; }

    private Dungeon(string title, int numberOfRooms, int numberOfMonsters)
    {
        Title = title;
        _rooms = GetRooms(numberOfRooms).ToArray() ?? throw new InvalidOperationException();
        _monsters = GetMonsters(numberOfMonsters).ToArray() ?? throw new InvalidOperationException();
    }

    private static Actions ShowMenu()
    {
        Console.WriteLine("What do you want to do? \n[Attack], [Block], [Run] Away, Player [Info], Look at [Monster], [Exit]");
        string str = Console.ReadLine() ?? string.Empty;
        str = str.ToLower().Trim();
        return str switch
        {
            "run" => Actions.RunAway,
            "block" => Actions.Block,
            "attack" => Actions.Attack,
            "info" => Actions.PlayerInfo,
            "monster" => Actions.MonsterInfo,
            "exit" => Actions.Exit,
            _ => Actions.Invalid
        };
    }

    private static IEnumerable<Character> GetMonsters(int numberOfMonsters)
    {
        var monsters = new List<Character>();
        var faker = new Faker();
        for (var i = 0; i < numberOfMonsters; i++)
        {
            monsters.Add(new Character(faker.Name.FirstName(), WeaponsHelper.GetWeapon()));
        }
        
        return monsters;
    }

    private static IEnumerable<Room> GetRooms(int numberOfRooms)
    {
        //TODO: Read from Rooms.csv or other source
        var faker = new Faker();
        var rooms = new List<Room>();
        for (var i = 0; i < numberOfRooms; i++)
        {
            rooms.Add(new Room(faker.Lorem.Sentence()));
        }

        return rooms;
    }

    private Character GetMonster()
    {
        var rand = new Random();
        return _monsters[rand.Next(_monsters.Length)];
    }

    private Room GetRoom()
    {
        var rand = new Random();
        return _rooms[rand.Next(_rooms.Length)];
    }

    private static Weapon DropLoot(int level)
    {
        Weapon baseWeapon = WeaponsHelper.GetWeapon();
        return new Weapon(baseWeapon, level);
    }
    
    public static void Main()
    {
        Console.WriteLine("What is your name?:");
        string? pcName = Console.ReadLine();
        Console.WriteLine("Do you wish to carry a shield into the dungeon? You will not be able to find another... Y/N");
        string? selection = Console.ReadLine();
        bool wantsShield = selection != null && selection.ToLower() == "y";

        //Set Randomizer and variables for Bogus
        Randomizer.Seed = new Random(0451);

        var dungeon = new Dungeon("Test Title", 10, 10);
        var playerCharacter = new Hero(pcName ?? "Hero", WeaponsHelper.GetWeapon(), wantsShield);
        var monstersKilled = 0;
        
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
                monstersKilled++;
                if (monstersKilled % 2 == 0)
                {
                    playerCharacter.LevelUp();
                }
                Weapon loot = DropLoot(monstersKilled);
                Console.WriteLine($"Victory! {currentMonster.Name} drops a {loot.Name}! \nDo you wish to equip this? Y/N"); 
                selection = Console.ReadLine();
                if (selection != null && selection.ToLower() == "y")
                {
                    playerCharacter.EquipWeapon(loot);
                }
                Console.WriteLine("Moving to the next room...");
                currentMonster = dungeon.GetMonster();
                currentRoom = dungeon.GetRoom();
                Console.WriteLine(currentRoom.Description);
                currentMonster.Info();
            }
            
            action = ShowMenu();
            switch (action)
            {
                case Actions.Invalid:
                    Console.WriteLine("I don't know what you mean.");
                    continue;
                case Actions.Attack:
                    currentMonster.TakeDamage(playerCharacter.Attack());
                    playerCharacter.TakeDamage(currentMonster.Attack());
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
                    if (!playerCharacter.IsBlocking)
                    {
                        Console.WriteLine(playerCharacter.CanBlock
                            ? "You prepare to block the next attack! Blocking halves the damage you take and receive!"
                            : "You cannot block!");
                        playerCharacter.IsBlocking = true;
                    }
                    else
                    {
                        Console.WriteLine("You lower your guard, taking and dealing normal damage.");
                        playerCharacter.IsBlocking = false;
                    }

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