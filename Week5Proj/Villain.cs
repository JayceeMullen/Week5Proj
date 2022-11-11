namespace Week5Proj;

public class Villain : Character
{
    public Villain(string name, Weapon equippedWpn)
        : base(name, equippedWpn)
    {
        
    }
    public void Monologue()
    {
        Console.WriteLine("Villain Monologue");
    }
}