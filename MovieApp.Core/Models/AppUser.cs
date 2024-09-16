using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        //relations
        public ICollection<Comment> Comments { get; set; }

    }
}
