using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;

public class BudgetCurrency : AbisEntityBase
{
    [Required, Display(Name = "Year"), Range(2010, 3000)]
    public int BudgetStartYear { get; set; }
    [Required, Display(Name = "Version"), Range(0, int.MaxValue)]
    public string? BudgetVersionName { get; set; }
    [Display(Name = "Document currency"), MaxLength(3)]   //Reference standard currency code from currencies table
    public string? DocumentCurrencyCode { get; set; }
    [Display(Name = "Local currency"), MaxLength(3)]   //Reference standard currency code from currencies table
    public string? LocalCurrencyCode { get; set; }
    [Required, Display(Name ="Article")] 
    public int ArticleFinalID { get; set; }
    public ArticleFinal? ArticleFinal { get; set; }
    [Required, Display(Name ="Customer")] 
    public int CustomerFinalID { get; set; }
    public CustomerFinal? CustomerFinal { get; set; }
    [Display(Name="Planning complete")]
    public bool PlanningComplete { get; set; }
}