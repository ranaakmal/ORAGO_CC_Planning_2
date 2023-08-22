using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

public class UploadLogHeader : AbisEntityBase
{
    [Required, Display(Name = "File name")]
    public string? FileName { get; set; }
    [JsonIgnore]
    public virtual ICollection<UploadLogItem>? UploadLogItems { get; set; }
}