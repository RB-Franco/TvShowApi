using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class User : IdentityUser
    {
        [Column("USR_NAME")]
        public string Name { get; set; }

        [Column("USR_CREATEDATE")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
