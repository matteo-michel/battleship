namespace BattleShip.Models.DTO.Output;

public class WarOutput
{
    public int Id { get; set; }
    public List<ShipOutput> Ships { get; set; }
}