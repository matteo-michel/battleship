namespace BattleShip.Models.DTO.Output;

public class BlastOutput
{
    public bool Over { get; set; }
    public Player? Winner { get; set; }
    public bool Hit { get; set; }
    public bool Sunken { get; set; }
    public AiBlastOutput AiBlast { get; set; }
}