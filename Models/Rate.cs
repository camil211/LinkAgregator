using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGen.Models
{
    public class Rate
    {
        public int RateId { get; set; }        

        public DateTime RateDate { get; set; }

        public Url Url { get; set; }

        public UserApplication User { get; set; }
    }   
}
