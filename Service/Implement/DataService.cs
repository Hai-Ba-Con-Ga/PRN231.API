namespace Service.Implement;

public class DataService
{
    public int CollectedDataTypeId { get; set; }
    public int DeviceId { get; set; }
    public string DataValue { get; set; } = null!;
    public string DataUnit { get; set; } = null!;
}