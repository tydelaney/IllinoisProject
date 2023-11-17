﻿using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.ViewModels
{
	public class AccountLoginViewModel 
	{ 
		//[Required, EmailAddress]
		public string Email { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }



    }
}
