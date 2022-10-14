using Bogus;

namespace Week5Proj;

public class Dungeon
{ 
    private string _title;
    private List<Room> _rooms;
    private List<Monster> _monsters;

    public Dungeon(string title, int numberOfRooms, int numberOfMonsters)
    {
        _title = title;
        _rooms = GetRooms(numberOfRooms) as List<Room> ?? throw new InvalidOperationException();
        _monsters = GetMonsters(numberOfMonsters) as List<Monster> ?? throw new InvalidOperationException();
    }

    public static Actions ShowMenu()
    {
        Console.WriteLine("Show Menu");
        return Actions.Exit;
    }

    private static IEnumerable<Monster> GetMonsters(int numberOfMonsters)
    {
        var monsters = new List<Monster>();
        for (var i = 0; i < numberOfMonsters; i++)
        {
            monsters.Add(new Monster(i.ToString()));
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
}