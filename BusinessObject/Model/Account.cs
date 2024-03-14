using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Model;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("AccountID")]
    public int AccountId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    [Column("ParentAccountID")]
    public int? ParentAccountId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
