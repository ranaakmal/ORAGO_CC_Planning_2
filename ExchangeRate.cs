using System.ComponentModel.DataAnnotations;

namespace ORAGO_CC_Planning.Models;
public class ExchangeRate : AbisEntityBase
{
    [Required, Display(Name = "From currency"), MaxLength(3)]   //Reference standard currency code from currencies table
    public string? FromCurrencyCode { get; set; }
    [Required, Display(Name = "To currency"), MaxLength(3)]   //Reference standard currency code from currencies table
    public string? ToCurrencyCode { get; set; }
    [Required, Display(Name = "Exchange year"), Range(2000, 3000)]
    public int ExchangeYear { get; set; }
    [Required, Display(Name = "Budget Exchange value"), Range(0.00, double.MaxValue)]
    public double ExchangeValue { get; set; }
    [Required, Display(Name = "Reporting Exchange value"), Range(0.00, double.MaxValue)]
    public double ReportingExchangeValue { get; set; }
}