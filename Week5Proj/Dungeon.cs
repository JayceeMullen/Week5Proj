using Bogus;

namespace Week5Proj;

public class Dungeon
{
    private readonly Room[] _rooms;
    private readonly Character[] _monsters;
    
    public string Title { get; }
    public Dungeon(string title, int numberOfRooms, int numberOfMonsters)
    {
        Title = title;
        _rooms = GetRooms(numberOfRooms).ToArray() ?? throw new InvalidOperationException();
        _monsters = GetMonsters(numberOfMonsters).ToArray() ?? throw new InvalidOperationException();
    }

    public static Actions ShowMenu()
    {
        Console.WriteLine("What do you want to do? \n[Attack], [Run] Away, Player [Info], Look at [Monster], [Exit]");
        string str = Console.ReadLine() ?? string.Empty;
        str = str.ToLower().Trim();
        return str switch
        {
            "run" => Actions.RunAway,
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
        for (var i = 0; i < numberOfMonsters; i++)
        {
            monsters.Add(new Character($"Monster {i}", WeaponsHelper.GetWeapon()));
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

    public Character GetMonster()
    {
        var rand = new Random();
        return _monsters[rand.Next(_monsters.Length)];
    }

    public Room GetRoom()
    {
        var rand = new Random();
        return _rooms[rand.Next(_rooms.Length)];
    }
}