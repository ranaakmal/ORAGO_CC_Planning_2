using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

public class UploadLogItem : AbisEntityBase
{
    [Required, Display(Name = "Log header")]
    public int UploadLogHeaderId { get; set; }
    public UploadLogHeader? UploadLogHeader { get; set; }
    [Required, Display(Name = "Row number"), Range(1, double.MaxValue)]
    public double FileRowNumber { get; set; }
    [Display(Name = "Entity")]
    public string? Entity { get; set; }
    [Display(Name = "Item No.")]
    public string? ItemNo { get; set; }
    [Display(Name = "BP-Code")]
    public string? BPCode { get; set; }
    [Display(Name = "DB ID")]
    public int DBID { get; set; }
    [Display(Name = "Result")]
    public string? Result { get; set; }
}