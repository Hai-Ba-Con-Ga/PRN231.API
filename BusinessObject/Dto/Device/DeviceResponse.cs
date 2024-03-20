using BusinessObject.Dto.Account;
using BusinessObject.Dto.DeviceType;

namespace BusinessObject.Dto.Device
{
    public class DeviceResponse
    {
        public string SerialId { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public int OwnerId { get; set; }
        public AccountResponse Owner { get; set; } = null!;
        public int DeviceTypeId { get; set; }
        public DeviceTypeResponse DeviceType { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }

    }
}
