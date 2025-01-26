namespace Handbook.Models;

internal sealed class Cylinder : DeviceBase
{
    public int Stroke { get; set; }
    public int Bore { get; set; }
    public int OuterDiameter { get; set; }
    public int ShrinkLength { get; set; }
    public int ExtendLength { get; set; }
    public int OilPortDistance { get; set; }

    public override string ToString()
    {
        return $"Stroke: {Stroke}, Bore: {Bore}, OuterDiameter: {OuterDiameter}, ShrinkLength: {ShrinkLength}, ExtendLength: {ExtendLength}, OilPortDistance: {OilPortDistance}, Uid: {Uid}";
    }
}
