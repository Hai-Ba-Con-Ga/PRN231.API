using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("CollectedDataType")]
public partial class CollectedDataType
{
    [Key]
    [Column("CollectedDataTypeID")]
    public int CollectedDataTypeId { get; set; }

    [StringLength(100)]
    public string DataTypeName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("CollectedDataType")]
    public virtual ICollection<CollectedDatum> CollectedData { get; set; } = new List<CollectedDatum>();
}
