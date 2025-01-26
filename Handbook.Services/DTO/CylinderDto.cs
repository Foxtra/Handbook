namespace Handbook.Services.DTO;

public sealed class CylinderDto
{
    public long Id { get; set; }
    public Guid Uid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Stroke { get; set; }
    public int Bore { get; set; }
    public int OuterDiameter { get; set; }
    public int ShrinkLength { get; set; }
    public int ExtendLength { get; set; }
    public int OilPortDistance { get; set; }
}
