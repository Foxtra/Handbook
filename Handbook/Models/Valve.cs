namespace Handbook.Models;

internal sealed class Valve : DeviceBase
{
    public double NominalPipeSize { get; set; }
    public int Globe { get; set; }
    public int BallCheck { get; set; }
    public int Angle { get; set; }
    public int SwingCheck { get; set; }
    public int PlugCock { get; set; }
    public int GateOrBallValve { get; set; }

    public override string ToString()
    {
        return $"NominalPipeSize: {NominalPipeSize}, Globe: {Globe}, BallCheck: {BallCheck}, Angle: {Angle}, SwingCheck: {SwingCheck}, PlugCock: {PlugCock}, GateOrBallValve: {GateOrBallValve}, Uid: {Uid}";
    }
}
