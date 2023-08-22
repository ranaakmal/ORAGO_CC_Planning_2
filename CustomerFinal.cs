using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

[Index(nameof(BpCode), IsUnique = true)]
public class CustomerFinal : AbisEntityBase
{
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
    [JsonIgnore]
    public virtual ICollection<BudgetEntry>? BudgetEntries { get; set; }
    [JsonIgnore]
    public virtual ICollection<BudgetCurrency>? BudgetCurrencies { get; set; }
}