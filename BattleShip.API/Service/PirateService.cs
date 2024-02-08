using BattleShip.Models;

namespace BattleShip.API.Service;

public class PirateService
{

    public Pirate instanciatePirate(string name, bool isBot = false)
    {
        Pirate pirate = new Pirate() { Name = name };
        pirate = generateFleet(pirate);

        if (isBot)
        {
            pirate.isBot = true;
            pirate = generateBlastLocations(pirate);
        }

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
    
    public Pirate generateBlastLocations(Pirate pirate)
    {
        pirate.BlastLocations = new List<int[]>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                pirate.BlastLocations.Add(new int[] {i, j});
            }
        }
        
        pirate.BlastLocations = pirate.BlastLocations.OrderBy(x => Random.Shared.Next()).ToList();
        return pirate;
    }
}