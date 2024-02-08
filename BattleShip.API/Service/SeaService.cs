using BattleShip.Models;
using BattleShip.Models.DTO.Output;

namespace BattleShip.API.Service;

public class SeaService
{
    
    public SeaOutput GetSeaHistory(Sea sea, int? round)
    {
        SeaOutput seaOutput = new SeaOutput();
        seaOutput.Hits = new List<HitOutput>();
        if (round is null)
        {
            round = sea.Hits.Count;
        }
        
        for (int i = 0; i < round; i++)
        {
            seaOutput.Hits.Add(new HitOutput
            {
                X = sea.Hits[i].X,
                Y = sea.Hits[i].Y,
                Reached = sea.Hits[i].Reached
            });
        }
        return seaOutput;
    }
    
}