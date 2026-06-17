using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class InvoiceModel
    {
        [Required(ErrorMessage = "The amount is required")] // حقل إجباري
        [Range(1, 100000, ErrorMessage = " 1 and 100,000 ")] // تحديد حدود الرقم
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The client's name is very important for the invoice")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 to 50 characters")]
        public string? CustomerName { get; set; }
    }
}
