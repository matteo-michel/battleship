using BattleShip.Models;

namespace BattleShip.API.Service;

public class PirateService
{

    public Pirate instanciatePirate(string name, bool isBot = false)
    {
        Pirate pirate = new Pirate() { Name = name };
        pirate = generateFleet(pirate);
        pirate.isBot = isBot;

        return pirate;
    }
    
    public Pirate generateFleet(Pirate pirate)
    {
        for (int i = 0; i < 5; i++)
        {
            pirate.Ships.Add(new Ship()
            {
                Letter = (char)(65 + i),
                Size = i + 1,
                Orientation = i % 2 == 0 ? 'H' : 'V'
            });
        }

        return pirate;
    }
    
    public Pirate GenerateBlastLocations(Pirate pirate, int size)
    {
        pirate.BlastLocations = new List<int[]>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                pirate.BlastLocations.Add(new int[] {i, j});
            }
        }
        
        pirate.BlastLocations = pirate.BlastLocations.OrderBy(x => Random.Shared.Next()).ToList();
        return pirate;
    }

    public Pirate ReorderBlastLocationsWithHitLocation(Pirate pirate, int x, int y)
    {
        // based on the current blast location, reorder the list to put the hit locations near x and y first
        pirate.BlastLocations!.Sort((pos1, pos2) =>
        {
            double distance1 = AbsoluteDistanceBetween2Positions(pos1, [x, y]);
            double distance2 = AbsoluteDistanceBetween2Positions(pos2, [x, y]);
            
            return distance1.CompareTo(distance2);
        });

        Console.WriteLine("Blast locations");
        foreach (var pos in pirate.BlastLocations)
        {
            Console.WriteLine($"({pos[0]}, {pos[1]})");
        }
        
        return pirate;
    }
    
    static double AbsoluteDistanceBetween2Positions(int[] pos1, int[] pos2)
    {
        int deltaX = pos1[0] - pos2[0];
        int deltaY = pos1[1] - pos2[1];
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public Pirate reoderBlastLocation(Pirate pirate)
    {
        pirate.BlastLocations = pirate.BlastLocations!.OrderBy(x => Random.Shared.Next()).ToList();
        return pirate;
    }
}