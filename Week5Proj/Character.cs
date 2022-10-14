using Bogus;

namespace Week5Proj;

public class Character
{
    private readonly string _name;
    private int _toHit;
    private int _hp;
    private Weapon _equippedWpn;

    public Character(Weapon equippedWpn)
    {
        _equippedWpn = equippedWpn;
        
        var faker = new Faker();
        var rand = new Random();

        _name = faker.Name.FirstName();
        _toHit = rand.Next(1, 5);
        _hp = rand.Next(10, 50);
    }
    public Character(string name, Weapon equippedWpn)
    {
        _name = name;
        _equippedWpn = equippedWpn;
    }

    public void Attack()
    {
        
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        _equippedWpn = newWeapon;

    }

    public override string ToString()
    {
        return $"{_name} has {_hp} hit points and is wielding a {_equippedWpn.Name} at +{_toHit} to hit";
    }
}