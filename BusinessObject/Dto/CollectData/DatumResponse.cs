using BusinessObject.Dto.Device;

namespace BusinessObject.Dto.CollectData;

public class DatumResponse
{
    public int CollectedDataId { get; set; }
    public string DataValue { get; set; } = null!;
    public string DataUnit { get; set; } = null!;
    public int DeviceId { get; set; }
    public DeviceResponse Device { get; set; } = null!;
    public int CollectedDataTypeId { get; set; }
    public DataTypeResponse CollectedDataType { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
}                                                                                     