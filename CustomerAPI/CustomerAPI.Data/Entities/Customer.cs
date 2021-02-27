using CustomerAPI.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Data
{
    public class Customer: EntityBase
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName{ get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName{ get; set; }

        [Phone(ErrorMessage = "Enter a valid phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber{ get; set; }

        [EmailAddress(ErrorMessage ="Please, enter a valid email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email{ get; set; }

        [Required]
        public int Age{ get; set; }
    }
}
