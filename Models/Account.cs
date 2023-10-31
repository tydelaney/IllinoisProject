
﻿using Microsoft.AspNetCore.Identity;

﻿using Azure.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using IllinoisProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.Models
{
    public class Account
    {

      
        public int AccountId { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public string AccountName { get; set; }
        public string UserName { get; set; }
      
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
    
}
