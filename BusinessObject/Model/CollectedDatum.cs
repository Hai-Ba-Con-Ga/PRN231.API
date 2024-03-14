using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

public partial class CollectedDatum
{
    [Key]
    [Column("CollectedDataID")]
    public int CollectedDataId { get; set; }

    [Column("CollectedDataTypeID")]
    public int CollectedDataTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DataValue { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DataUnit { get; set; } = null!;

    [Column("DeviceID")]
    public int DeviceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("CollectedDataTypeId")]
    [InverseProperty("CollectedData")]
    public virtual CollectedDataType CollectedDataType { get; set; } = null!;

    [ForeignKey("DeviceId")]
    [InverseProperty("CollectedData")]
    public virtual Device Device { get; set; } = null!;
}
