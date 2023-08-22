using System.ComponentModel.DataAnnotations;

namespace ORAGO_CC_Planning.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Comments { get; set; }
    }
}
