namespace Handbook.Services.DTO;

public sealed class PumpDto
{
    public long Id { get; set; }
    public Guid Uid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public double Head { get; set; }
    public int Speed { get; set; }
    public int Efficiency { get; set; }
    public double MotorPower { get; set; }
}
