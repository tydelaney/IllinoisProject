
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
        public string AccountEmail { get; set; }
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string? UserId { get; set; }
        public Picture? Picture { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
    
}
