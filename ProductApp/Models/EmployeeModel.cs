using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Id")]
        public int Empid { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City must be 100 characters or fewer.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;
    }
}
