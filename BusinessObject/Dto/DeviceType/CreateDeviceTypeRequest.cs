using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.DeviceType
{
    public class CreateDeviceTypeRequest
    {
        [StringLength(100)]
        public string TypeName { get; set; } = null!;
    }
}
