using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("Device")]
public partial class Device
{
    [Key]
    [Column("DeviceID")]
    public int DeviceId { get; set; }

    [Column("SerialID")]
    [StringLength(100)]
    [Unicode(false)]
    public string SerialId { get; set; } = null!;

    [StringLength(100)]
    public string DeviceName { get; set; } = null!;

    [Column("OwnerID")]
    public int OwnerId { get; set; }

    [Column("DeviceTypeID")]
    public int DeviceTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("Device")]
    public virtual ICollection<CollectedDatum> CollectedData { get; set; } = new List<CollectedDatum>();

    [ForeignKey("DeviceTypeId")]
    [InverseProperty("Devices")]
    public virtual DeviceType DeviceType { get; set; } = null!;

    [ForeignKey("OwnerId")]
    [InverseProperty("Devices")]
    public virtual Account Owner { get; set; } = null!;
}
