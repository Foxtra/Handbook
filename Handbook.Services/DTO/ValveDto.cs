namespace Handbook.Services.DTO;

public sealed class ValveDto
{
    public long Id { get; set; }
    public Guid Uid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public double NominalPipeSize { get; set; }
    public int Globe { get; set; }
    public int BallCheck { get; set; }
    public int Angle { get; set; }
    public int SwingCheck { get; set; }
    public int PlugCock { get; set; }
    public int GateOrBallValve { get; set; }
}
