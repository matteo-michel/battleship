namespace BattleShip.Models.DTO.Output;

public class BlastOutput
{
    public bool Hit { get; set; }
    public bool Sunken { get; set; }
    public int[] Position { get; set; }
    
    public BlastOutput(bool hit, bool sunken, int[] position)
    {
        Hit = hit;
        Sunken = sunken;
        Position = position;
    }
}