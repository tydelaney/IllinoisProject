using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace IllinoisProject.ViewModels
{
	public class AccountRegisterViewModel
	{
		[Required, MaxLength(256), EmailAddress]
		[Display(Name = "Email Address")]
		[DomainValidation(AllowedDomain = "illinois.edu", ErrorMessage = "Only illinois.edu emails are allowed.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
		[MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[MinLength(6, ErrorMessage = "Confirm Password must be at least 6 characters")]
		[MaxLength(20, ErrorMessage = "Confirm Password cannot exceed 20 characters")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }


		public Account Account { get; set; }
		//public SelectList AccountList { get; set; }
		//public BlogPost BlogPost { get; set; }

	}

	public class DomainValidationAttribute : ValidationAttribute
	{
		public string AllowedDomain { get; set; }

		public override bool IsValid(object value)
		{
			string[] strings = value.ToString().Split('@');
			return strings.Length > 1 && strings[1].Equals(AllowedDomain, StringComparison.OrdinalIgnoreCase);
		}
	}

}
