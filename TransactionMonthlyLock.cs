using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;

public class TransactionMonthlyLock : AbisEntityBase
{
    [Required, Display(Name = "Year"), Range(2010, 3000)]
    public int LockYear { get; set; }    
    [Required, Display(Name = "Month"), Range(1, 12)]
    public int LockMonth { get; set; }

    [Required, Display(Name = "Lock status")]
    public bool IsLocked { get; set; }
}