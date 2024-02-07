using BattleShip.Models;

namespace BattleShip.Models.DTO.Output;

public class ShipOutput
{

    public string Letter { get; set; }
    public int[][] Positions { get; set; }
}