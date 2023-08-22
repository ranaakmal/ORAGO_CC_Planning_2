using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;
//This class represents the budget header. 
public class YearlyLock : AbisEntityBase
{
    [Required, Display(Name = "Year"), Range(2010, 3000)]
    public int LockYear { get; set; }

    [Required, Display(Name = "Version"), Range(0, int.MaxValue)]
    public int BudgetVersion { get; set; }

    [Required, Display(Name = "Locked")]
    public bool IsLocked { get; set; }
    public string? Description { get; set; }
    [Required, Display(Name ="Active forecast"), Range(1, 12)]
    public int ActiveForecast { get; set; }
}