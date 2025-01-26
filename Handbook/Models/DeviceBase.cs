namespace Handbook.Models;

internal abstract class DeviceBase
{
    public long Id { get; set; }
    public Guid Uid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}
