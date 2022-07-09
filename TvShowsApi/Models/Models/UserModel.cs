using Microsoft.AspNetCore.Identity;
using System;

namespace Models.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
