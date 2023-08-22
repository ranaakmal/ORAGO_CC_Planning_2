using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;

public class BudgetEntry : AbisEntityBase
{
    [Required, Display(Name = "Budget start year"), Range(2010, 3000)]
    
    public int BudgetStartYear { get; set; }
    [Required, Display(Name = "Version")]
    public string? BudgetVersionName { get; set; }
    [Required, Display(Name = "Duration number"), Range(1, 36)]
    
    public int DurationNumber { get; set; }
    [Required, Display(Name = "Month Nr."), Range(1, 12)]
    
    private int _monthNumber;
    public int MonthNumber
    {
        get
        {
            if (DurationNumber > 24 ) return DurationNumber - 24;
            if (DurationNumber > 12 ) return DurationNumber - 12;
            return DurationNumber;
        }
        set { }
    }
    [Required, Display(Name = "Month"), Range(1, 12)]
    private string? _monthName;
    public string? MonthName
    {
        get
        {
            return DateTime.Parse("2000, " + (this.MonthNumber).ToString() + ", 1").ToString("MMM");
        }
        set { }
    }
    [Required, Display(Name = "Year"), Range(2009, 3002)] 
    
    private int _yearNumber;
    public int YearNumber
    {
        get
        {
            if (DurationNumber > 24) return BudgetStartYear + 1;
            if (DurationNumber > 12) return BudgetStartYear;
            return BudgetStartYear - 1;
        }
        set { }
    }
    [Required, Display(Name = "Volume %age"), Range(0, 1)]
    
    public double VolumePercentage { get; set; }
    [Required, Display(Name ="Article")] 
    public int ArticleFinalID { get; set; }
    public ArticleFinal? ArticleFinal { get; set; }
    [Required, Display(Name ="Customer")] 
    public int CustomerFinalID { get; set; }
    public CustomerFinal? CustomerFinal { get; set; }
    [Required, Display(Name = "Qty. "), Range(0, double.MaxValue)] 
    public double FC1Quantity { get; set; }
    [Required, Display(Name = "Price"), Range(0, double.MaxValue)] 
    public double FC1Price { get; set; }
    [Required, Display(Name = "Surcharge"), Range(0, double.MaxValue)] 
    public double FC1Surcharge { get; set; }
    [Display(Name = "Sales"), Range(0, double.MaxValue)] 
    private double _fC1Sales;
    public double FC1Sales
    {
        get
        {
            return (FC1Price + FC1Surcharge) * FC1Quantity;
        }
        set { }
    }
    private bool _fC1Allowed;
    public bool FC1Allowed
    {
        get
        {
            if (DurationNumber >= 4 && DurationNumber <= 12) return true;
            return false;
        }
        set { }
    }
    [Required, Display(Name = "Qty. "), Range(0, double.MaxValue)] 
    public double FC2Quantity { get; set; }
    [Required, Display(Name = "Price"), Range(0, double.MaxValue)] 
    public double FC2Price { get; set; }
    [Required, Display(Name = "Surcharge"), Range(0, double.MaxValue)] 
    public double FC2Surcharge { get; set; }
    [Display(Name = "Sales"), Range(0, double.MaxValue)] 
    private double _fC2Sales;
    public double FC2Sales
    {
        get
        {
            return (FC2Price + FC2Surcharge) * FC2Quantity;
        }
        set { }
    }
    public bool _fC2Allowed;
    public bool FC2Allowed
    {
        get
        {
            if (DurationNumber >= 7 && DurationNumber <= 12) return true;
            return false;
        }
        set { }
    }
    [Required, Display(Name = "Qty. "), Range(0, double.MaxValue)] 
    public double FC3Quantity { get; set; }
    [Required, Display(Name = "Price"), Range(0, double.MaxValue)] 
    public double FC3Price { get; set; }
    [Required, Display(Name = "Surcharge"), Range(0, double.MaxValue)] 
    public double FC3Surcharge { get; set; }
    [Display(Name = "Sales"), Range(0, double.MaxValue)] 
    private double _fC3Sales;
    public double FC3Sales
    {
        get
        {
            return (FC3Price + FC3Surcharge) * FC3Quantity;
        }
        set { }
    }
    private bool _fC3Allowed;
    public bool FC3Allowed
    {
        get
        {
            if (DurationNumber >= 10 && DurationNumber <= 12) return true;
            return false;
        }
        set { }
    }
    [Required, Display(Name = "Qty. "), Range(0, double.MaxValue)] 
    public double BudgetQuantity { get; set; }
    [Required, Display(Name = "Price"), Range(0, double.MaxValue)] 
    public double BudgetPrice { get; set; }
    [Required, Display(Name = "Surcharge"), Range(0, double.MaxValue)] 
    public double BudgetSurcharge { get; set; }
    [Display(Name = "Sales"), Range(0, double.MaxValue)] 
    private double _budgetSales;
    public double BudgetSales
    {
        get
        {
            return (BudgetPrice + BudgetSurcharge) * BudgetQuantity;
        }
        private set { }
    }
    private bool _budgetAllowed;
    public bool BudgetAllowed
    {
        get
        {
            if (DurationNumber >= 13) return true;
            return false;
        }
        set { } 
    }
}