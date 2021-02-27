using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Data.Models
{
    public class CustomerModel
    {
        [Display(Name ="First Name")]
        [Required]
        public string FirstName { get; set; }
        
        [Display(Name ="Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1, 120, ErrorMessage ="Age valid must be between 1 and 120")]
        public int Age { get; set; }
    }
}
