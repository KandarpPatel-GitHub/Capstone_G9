using System;
using System.ComponentModel.DataAnnotations;

namespace ChefConnect.Models
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage ="Username can't be empty")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date can't be empty")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be empty")]
        [DataType(DataType.Password)]
        [Compare("Password")]
		public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Phone number can't be empty")]
        [StringLength(10,ErrorMessage ="Please enter a vaild Phone Number")]
		public string PhoneNumber { get; set; }
	}
}

