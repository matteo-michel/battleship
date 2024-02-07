namespace BattleShip.Models;

public class Player
{
    public string Name { get; set; }
    public List<Ship> Ships { get; set; }
    
    public Player(string name)
    {
        Name = name;
        
        // Create a new list of ships with letter A and length 1 and increment it to 5
        Ships = new List<Ship>();
        for (int i = 0; i < 5; i++)
        {
            Ships.Add(new Ship((char)(65 + i), i + 1, i % 2 == 0 ? 'H' : 'V'));
        }
    }
}