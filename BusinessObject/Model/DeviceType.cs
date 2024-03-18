using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("DeviceType")]
public partial class DeviceType
{
    [Key]
    [Column("DeviceTypeID")]
    public int DeviceTypeId { get; set; }

    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("DeviceType")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
