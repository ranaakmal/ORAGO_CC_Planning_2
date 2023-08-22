using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

[Index(nameof(BpCode), IsUnique = true)]
[Index(nameof(BpName), IsUnique = true)]
public class Customer : AbisEntityBase
{
    //[Required]
    //[MaxLength(25)]
    //[Display(Name = "Customer number")]
    //public string? CustomerNumber {get; set; }
    //[Required]
    //[MaxLength(25)]
    //[Display(Name = "City")]
    //public string? CustomerCity {get; set; }
    //[Required]
    //[Display(Name = "Key customer")]
    //public int KeyCustomerID { get; set; }
    //public KeyCustomer? KeyCustomer { get; set; }
    //[Required]
    //[Display(Name = "Customer group")]
    //public int L2CustomerGroupID { get; set; }
    //public L2CustomerGroup? L2CustomerGroup { get; set; }
    //[Required]
    //[Display(Name = "Sales manager")]
    //public int SalesManagerID { get; set; }
    //public SalesManager? SalesManager { get; set; }
    //[Required]
    //[Display(Name = "CS representative")]
    //public int CSrepresentativeID { get; set; }
    //public CSRepresentative? CSRepresentative { get; set; }
    //[Required]
    //[Display(Name = "Origin")]
    //public int InternalCompanyID { get; set; }
    //public InternalCompany? InternalCompany { get; set; }
    //[Display(Name = "Payment terms (days)")]
    //public int PaymentTermDays { get; set; }
    //[JsonIgnore]
    //public virtual ICollection<Article>? Articles { get; set; }
    //  [Column(Order =0), Key]
    // public int Entity_Id{get; set;}
    [Required, MaxLength(50)]
    [Column(Order = 2), Display(Name="Entity*")]
    public string? Entity { get; set; }
    [Required, MaxLength(50), Display(Name = "BP-Code*")]
    [Column("BP-Code", Order = 3)]
    public string? BpCode { get; set; }
    [Required, MaxLength(150), Display(Name = "BP-Name*")]
    [Column("BP-Name", Order = 4)]
    public string? BpName { get; set; }
    [MaxLength(50), Display(Name = "Key customer")]
    [Column("Key Customer", Order = 5)]
    public string? KeyCustomer { get; set; }
    [MaxLength(50)]
    [Column("Incoterm", Order = 6)]
    public string? Incoterm { get; set; }
    [MaxLength(150), Display(Name="Assigned user")]
    public string? AssignedUser { get; set; }
    [MaxLength(150)]
    public string? KAM { get; set; }
}