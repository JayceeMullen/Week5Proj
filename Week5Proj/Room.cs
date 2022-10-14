namespace Week5Proj;

public class Room
{
    private readonly string _description;
    
    public string Description
    {
        get { return _description; }
    }

    public Room(string description)
    {
        _description = description;
    }
}