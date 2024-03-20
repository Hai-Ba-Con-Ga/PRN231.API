using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.DeviceType
{
    public class DeviceTypeResponse
    {
        public int DeviceTypeId { get; set; }

        public string TypeName { get; set; }  

        public int? NumberOfDevices { get; set; } = null;
    }
}
