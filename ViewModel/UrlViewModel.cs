using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGen.ViewModel
{
    public class UrlViewModel
    {
        public int UrlId { get; set; }

        [Required]
        [StringLength(1000)]
        [Url]
        [Display(Name = "Url address")]
        public string UrlLink { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(300)]
        [Display(Name = "Description")]
        public string Desc { get; set; }

        public int Rating { get; set; }

        public bool AllowRate { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

    }
}
