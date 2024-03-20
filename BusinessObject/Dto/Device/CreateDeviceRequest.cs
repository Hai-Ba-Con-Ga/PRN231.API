using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dto.Device
{
    public class CreateDeviceRequest
    {
        [Column("SerialID")]
        [StringLength(100)]
        [Unicode(false)]
        public string SerialId { get; set; } = null!;

        [StringLength(100)]
        public string DeviceName { get; set; } = null!;

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int DeviceTypeId { get; set; }
    }
}
