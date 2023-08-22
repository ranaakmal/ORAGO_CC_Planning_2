using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORAGO_CC_Planning.Models;
  
public class VolumeTemplate : AbisEntityBase
{

    [Display(Name = "Jan"), Range(0.00, 100)]
    public double Month1Percentage { get; set; }
    [Display(Name = "Feb"), Range(0.00, 100)]
    public double Month2Percentage { get; set; }
    [Display(Name = "Mar"), Range(0.00, 100)]
    public double Month3Percentage { get; set; }
    [Display(Name = "Apr"), Range(0.00, 100)]
    public double Month4Percentage { get; set; }
    [Display(Name = "May"), Range(0.00, 100)]
    public double Month5Percentage { get; set; }
    [Display(Name = "Jun"), Range(0.00, 100)]
    public double Month6Percentage { get; set; }
    [Display(Name = "Jul"), Range(0.00, 100)]
    public double Month7Percentage { get; set; }
    [Display(Name = "Aug"), Range(0.00, 100)]
    public double Month8Percentage { get; set; }
    [Display(Name = "Sep"), Range(0.00, 100)]
    public double Month9Percentage { get; set; }
    [Display(Name = "Oct"), Range(0.00, 100)]
    public double Month10Percentage { get; set; }
    [Display(Name = "Nov"), Range(0.00, 100)]
    public double Month11Percentage { get; set; }
    [Display(Name = "Dec"), Range(0.00, 100)]
    public double Month12Percentage { get; set; }
    [Display(Name = "Year total")]
    public double YearTotal
    {
        get
        {
            return Month1Percentage + Month2Percentage + Month3Percentage + Month4Percentage + Month5Percentage + Month6Percentage +
                    Month7Percentage + Month8Percentage + Month9Percentage + Month10Percentage + Month11Percentage + Month12Percentage;
        }
    }
} 