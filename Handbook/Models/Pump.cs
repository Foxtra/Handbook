namespace Handbook.Models;

internal sealed class Pump : DeviceBase
{
    public int Capacity { get; set; }
    public double Head { get; set; }
    public int Speed { get; set; }
    public int Efficiency { get; set; }
    public double MotorPower { get; set; }

    public override string ToString()
    {
        return $"Capacity: {Capacity}, Head: {Head}, Speed: {Speed}, Efficiency: {Efficiency}, MotorPower: {MotorPower}, Uid: {Uid}";
    }
}
