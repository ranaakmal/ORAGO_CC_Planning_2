using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

[Index(nameof(ItemNo), IsUnique = true)]
// public int Entity_Id {get;set;}
public class Article : AbisEntityBase
{
    //[MaxLength(150)]
    //[Display(Name = "Cust. Art. Nr. ")]
    //public string? CustomerArticleNumber { get; set; }
    //[Required]
    //[MaxLength(150)]
    //[Display(Name = "Description*")]
    //public string? ArticleDescription { get; set; }
    //[Required]
    //[Display(Name ="Unit *")]
    //public int MeasuringUnitID { get; set; }
    //public MeasuringUnit? MeasuringUnit { get; set; }
    //[Required]
    //[Range(0.0, double.MaxValue)]
    //[Display(Name ="Mass/unit *")]
    //public double MassPerUnit { get; set; }
    //[Required]
    //[Display(Name ="Prod. loc. *")]
    //public int ProductionLocationID { get; set; }
    //public ProductionLocation? ProductionLocation { get; set; }
    //[Required]
    //[Display(Name ="Prod. Gp. 1 *")]
    //public int ProductGroupID { get; set; }
    //public ProductGroup? ProductGroup { get; set; }
    //[Required]
    //[Display(Name ="Prod. Gp. 2 *")]
    //public int AdditionalProductGroupID { get; set; }
    //public AdditionalProductGroup? AdditionalProductGroup { get; set; }
    //[Required]
    //[Display(Name ="Packaging type *")]
    //public int PackagingTypeID { get; set; }
    //public PackagingType? PackagingType { get; set; }
    //[Required, Display(Name ="Assigned customer")]
    //public int CustomerID { get; set; }
    //public Customer? Customer { get; set; }
    //[JsonIgnore]
    //public virtual ICollection<BudgetPeriodEntry>? BudgetPeriodEntries { get; set; }
     

    // [Column(Order =0), Key]
    // public int Entity_Id{get; set;}
    [Required, MaxLength(50)]
    [Column(Order = 2), Display(Name ="Entity*")]
    public string? Entity { get; set; }
    [Required, MaxLength(150), Display(Name = "Item Nr.*")]
    [Column("Item No.", Order = 3)]
    public string? ItemNo { get; set; }
    [MaxLength(150), Display(Name = "Drawing Nr.")]
    [Column("Drawing No.", Order = 4)]
    public string? DrawingNo { get; set; }
    [MaxLength(150), Display(Name = "Item Nr. Ext.")]
    [Column("Item No. External", Order = 5)]
    public string? ItemNoExternal { get; set; }
    [MaxLength(150), Display(Name = "Description")]
    [Column("Item Description", Order = 6)]
    public string? ItemDescription { get; set; }
    [Column(Order = 7)] 
    public double Weight { get; set; }
    [MaxLength(10)]
    [Column(Order = 8)]
    public string? BUoM { get; set; }
    [MaxLength(50)]
    [Column(Order = 9)]
    public string? Manufacturer { get; set; }
    [MaxLength(150)]
    [Column(Order = 10)]
    public string? Customer { get; set; }
    [MaxLength(50), Display(Name = "Material group")]
    [Column("Material group", Order = 11)]
    public string? Materialgroup { get; set; }
    [MaxLength(50), Display(Name = "Prod. category")]
    [Column("Product category", Order = 12)]
    public string? Productcategory { get; set; }
    [MaxLength(50), Display(Name = "Business area")]
    [Column("Business area", Order = 13)]
    public string? BusinessArea { get; set; }
    [MaxLength(50)]
    [Column(Order = 14)]
    public string? Oem { get; set; }
    [MaxLength(50), Display(Name = "Car type")]
    [Column("Car type", Order = 15)]
    public string? CarType { get; set; }
    [MaxLength(10)]
    [Column(Order = 16)]
    public string? Sop { get; set; }
    [MaxLength(10)]
    [Column(Order = 17)]
    public string? Eop { get; set; }
    [Display(Name = "Yearly Qty.")]
    [Column("Yearly Quantity", Order = 18)] 
    public double YearlyQuantity { get; set; }
    [Display(Name = "Dummy")]
    public bool IsDummy { get; set; }
    [Display(Name="BP-Code for dummy")]
    public string? BPCodeDummy { get; set; }
}