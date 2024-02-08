namespace BattleShip.Models.DTO.Output;

public class AiBlastOutput
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Hit { get; set; }
    public bool Sunken { get; set; }
}