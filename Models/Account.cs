
﻿using Microsoft.AspNetCore.Identity;

﻿using Azure.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using IllinoisProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.Models
{
    public class Account : IdentityUser
    {
        public string Name { get; set; }
        public Picture? Picture { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }
        public Account()
        {
            BlogPosts = new List<BlogPost>();
        }
    }
    
}
