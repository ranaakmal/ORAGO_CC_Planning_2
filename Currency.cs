using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;

//Currency table is used in many tables and therefore we have not created a direct link with any table.
//Its entities will be refrenced at runtime via the ISO-Code column.  
public class Currency :AbisEntityBase
{
    [Required]
    [MaxLength(3)]
    [Display(Name = "ISO-code")]
    public string? ISOCode { get; set; }
} 