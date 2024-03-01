using System.Drawing;
using BattleShip.Models;
using BattleShip.Models.DTO.Input;

namespace BattleShip.API.Service;

public class ShipService
{
    
    public Ship CreateShipFromShipInput(ShipInput ship)
    {
        return new Ship()
        {
            Letter = ship.Letter,
            Size = ship.Positions.Length,
            Orientation = ship.Orientation,
            Positions = ship.Positions.Select(p => new ShipPosition() { X = p[0], Y = p[1]}).ToList()
        };
    }
}