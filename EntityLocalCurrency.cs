using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORAGO_CC_Planning.Models;

public class EntityLocalCurrency :AbisEntityBase
{
     [Required, MaxLength(50)]
    [Column(Order = 2), Display(Name="Entity*")]
    public string? Entity { get; set; }
    [Column(Order = 3), Display(Name = "Local currency"), MaxLength(3)]
    public string? LocalCurrencyCode { get; set; } 
    [Column(Order = 4), Display(Name = "Default Document currency"), MaxLength(3)]
    public string? DocumentCurrencyCode { get; set; }
}