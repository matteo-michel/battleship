using BattleShip.Models;
using BattleShip.Models.DTO.Output;

namespace BattleShip.API.Service;

public class SeaService
{
    
    public Sea CreateSea(int size, Pirate pirate)
    {
        Sea sea = new Sea()
        {
            Size = size,
            Grid = new char[size, size],
            Pirate = pirate
        };
        
        sea = FillSeaWithEmptyValue(sea);
        sea = GenerateRandomGrid(sea);
        return sea;
    }

    public Sea CloneSea(Sea sea)
    {
        var newSea = new Sea()
        {
            Size = sea.Size,
            Grid = new char[sea.Size, sea.Size],
            Pirate = new Pirate() { Name = sea.Pirate.Name, Ships = new List<Ship>() }
        };
        for (int i = 0; i < sea.Size; i++)
        {
            for (int j = 0; j < sea.Size; j++)
            {
                newSea.Grid[i, j] = sea.Grid[i, j];
            }
        }
        return newSea;
    }
    
    public Sea FillSeaWithEmptyValue(Sea sea)
    {
        for (int i = 0; i < sea.Size; i++)
        {
            for (int j = 0; j < sea.Size; j++)
            {
                sea.Grid[i, j] = '\0';
            }
        }
        return sea;
    }
    
    // generate random position for ships
    public Sea GenerateRandomGrid(Sea sea)
    {
        foreach (Ship ship in sea.Pirate.Ships)
        {
            bool placed = false;
            while (!placed)
            {
                int x = Random.Shared.Next(0, sea.Size);
                int y = Random.Shared.Next(0, sea.Size);
                if (ship.Orientation == 'H')
                {
                    if (x + ship.Size < sea.Size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.Size; i++)
                        {
                            if (sea.Grid[x + i, y] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int i = 0; i < ship.Size; i++)
                            {
                                ship.Positions.Add(new ShipPosition()
                                {
                                    X = x + i,
                                    Y = y,
                                    IsHit = false
                                });
                                sea.Grid[x + i, y] = ship.Letter;
                            }
                            placed = true;
                        }
                    }
                }
                else
                {
                    if (y + ship.Size < sea.Size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.Size; i++)
                        {
                            if (sea.Grid[x, y + i] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int i = 0; i < ship.Size; i++)
                            {
                                ship.Positions.Add(new ShipPosition()
                                {
                                    X = x,
                                    Y = y + i,
                                    IsHit = false
                                });
                                sea.Grid[x, y + i] = ship.Letter;
                            }
                            placed = true;
                        }
                    }
                }
            }
        }
        return sea;
    }
    
    public bool IsHitInHistory(Sea sea, int x, int y)
    {
        foreach (Hit hit in sea.Hits)
        {
            if (hit.X == x && hit.Y == y)
            {
                return true;
            }
        }
        return false;
    }
    
    public Ship? Hit(Sea sea, int x, int y)
    {
        sea.Hits.Add(new Hit()
        {
            X = x,
            Y = y,
            Reached = sea.Grid[x, y] != '\0'
        });
        
        if (sea.Grid[x, y] != '\0')
        {
            foreach (Ship ship in sea.Pirate.Ships)
            {
                if (ship.Letter == sea.Grid[x, y] && !ship.IsSunken())
                {
                    ship.Hits++;
                    return ship;
                }
            }
        }

        return null;
    }
    
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