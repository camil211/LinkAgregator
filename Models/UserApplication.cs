using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGen.Models
{
    public class UserApplication : IdentityUser
    {
        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<Url> Urls { get; set; }
    }
}
