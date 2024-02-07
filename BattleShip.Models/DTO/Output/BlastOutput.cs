namespace BattleShip.Models.DTO.Output;

public class BlastOutput
{
    public bool Hit { get; set; }
    public bool Sunken { get; set; }
    public int[] Position { get; set; }
}