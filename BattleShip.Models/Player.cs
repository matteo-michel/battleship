namespace BattleShip.Models;

public class Player
{
    
    public string name { get; set; }
    public List<Ship> ships { get; set; }
    
    public Player(string name)
    {
        this.name = name;
        
        // Create a new list of ships with letter A and length 1 and increment it to 5
        this.ships = new List<Ship>();
        for (int i = 0; i < 5; i++)
        {
            this.ships.Add(new Ship((char)(65 + i), i + 1, i % 2 == 0 ? 'H' : 'V'));
        }
    }
}