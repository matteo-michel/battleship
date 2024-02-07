namespace BattleShip.Models;

public class Navy: Player
{
    public List<int[]> BlastLocations { get; set; }
    
    public Navy(string name) : base(name)
    {
        // Create a new list of ships with letter A and length 1 and increment it to 5
        Ships = new List<Ship>();
        for (int i = 0; i < 5; i++)
        {
            Ships.Add(new Ship((char)(65 + i), i + 1, i % 2 == 0 ? 'H' : 'V'));
        }
        
        BlastLocations = new List<int[]>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                BlastLocations.Add(new int[] {i, j});
            }
        }
        
        BlastLocations = BlastLocations.OrderBy(x => Random.Shared.Next()).ToList();
    }
}