using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.CollectData
{
    public class CollectedDataRequest
    {
        public int CollectedDataTypeId { get; set; }
        public int DeviceId { get; set; }
        public string SerialId { get; set; } = null!;
        public string DataValue { get; set; } = null!;
        public string DataUnit { get; set; } = null!;
    }
}