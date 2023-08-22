using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ORAGO_CC_Planning.Models;

public class Transaction : AbisEntityBase
{
    //  [Column(Order =0), Key]
    // public int Entity_Id{get; set;}
    [Required, MaxLength(50)]
    [Column(Order = 2)]
    public string? Entity { get; set; }
    [Required, MaxLength(2)]
    [Column(Order = 3)]
    public string? ShipTo { get; set; }
    [Required, MaxLength(2)]
    [Column(Order = 4)]
    public string? SoldTo { get; set; }
    [Required, MaxLength(50)]
    [Column("BP-Code", Order = 5)]
    public string? BpCode { get; set; }
    [Required, MaxLength(300)]
    [Column("Document No.", Order = 6)]
    public string? DocumentNo { get; set; }
    [Required, MaxLength(2)]
    [Column(Order = 7)]
    public string? Month { get; set; }
    [Required, MaxLength(4)]
    [Column(Order = 8)]
    public string? Year { get; set; }
    [Required, MaxLength(150)]
    [Column("Item No.", Order = 9)]
    public string? ItemNo { get; set; }
    [Required]
    [Column(Order = 10)] 
    public double Quantity { get; set; }
    [Required]
    [Column("Sales - DC", Order = 11)] 
    public double SalesDc { get; set; }
    [Required]
    [Column("Alloy Surcharge 1 - DC", Order = 12)] 
    public double AlloySurcharge1Dc { get; set; }
    [Required]
    [Column("Alloy Surcharge 2 - DC", Order = 13)] 
    public double AlloySurcharge2Dc { get; set; }
    [Required]
    [Column("Surcharge - DC", Order = 14)] 
    public double SurchargeDc { get; set; }
    [Required, MaxLength(3)]
    [Column("Currency - DC", Order = 15)]
    public string? CurrencyDc { get; set; }
    [Required]
    [Column("Sales - LC", Order = 16)] 
    public double SalesLc { get; set; }
    [Required]
    [Column("Alloy Surcharge 1 - LC", Order = 17)] 
    public double AlloySurcharge1Lc { get; set; }
    [Required]
    [Column("Alloy Surcharge 2 - LC", Order = 18)] 
    public double AlloySurcharge2Lc { get; set; }
    [Required]
    [Column("Surcharge - LC", Order = 19)] 
    public double SurchargeLc { get; set; }
    [Required, MaxLength(3)]
    [Column("Currency - LC", Order = 20)]
    public string? CurrencyLc { get; set; }
}