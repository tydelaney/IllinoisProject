﻿using System.ComponentModel.DataAnnotations;

namespace Dropbox14.ViewModels
{
	public class AccountLoginViewModel
	{
		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }



    }
}
