using BattleShip.Models;

namespace BattleShip.API.DTO.Output;

public class ShipOutput
{

    public string Letter { get; set; }
    public int[][] Positions { get; set; }
    
    public ShipOutput(Ship ship)
    {
        Letter = ship.Letter.ToString();
        
        // create a new array with the position of the ship with the length of the ship and the orientation
        Positions = new int[ship.Size][];
        if (ship.Orientation == 'H')
        {
            for (int i = 0; i < ship.Size; i++)
            {
                Positions[i] = new int[] {ship.Position[0] + i, ship.Position[1]};
            }
        }
        else
        {
            for (int i = 0; i < ship.Size; i++)
            {
                Positions[i] = new int[] {ship.Position[0], ship.Position[1] + i};
            }
        }
    }


}