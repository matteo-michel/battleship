using BattleShip.Models;

namespace BattleShip.API.DTO.Output;

public class ShipOutput
{

    public string letter { get; set; }
    public int[][] positions { get; set; }
    
    public ShipOutput(Ship ship)
    {
        this.letter = ship.letter.ToString();
        
        // create a new array with the position of the ship with the length of the ship and the orientation
        this.positions = new int[ship.size][];
        if (ship.orientation == 'H')
        {
            for (int i = 0; i < ship.size; i++)
            {
                this.positions[i] = new int[] {ship.position[0] + i, ship.position[1]};
            }
        }
        else
        {
            for (int i = 0; i < ship.size; i++)
            {
                this.positions[i] = new int[] {ship.position[0], ship.position[1] + i};
            }
        }
    }


}