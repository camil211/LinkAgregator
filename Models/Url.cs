using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGen.Models
{
    public class Url
    {
        public int UrlId { get; set; }
        
        public string UrlLink { get; set; }
        
        public string Title { get; set; }
        
        public string Desc { get; set; }

        public int Rating { get; set; }
        
        public DateTime CreateDate { get; set; }

        public virtual UserApplication User { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
