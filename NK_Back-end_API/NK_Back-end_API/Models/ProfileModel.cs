using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Models
{
    public class ProfileModel
    {
        [Required]
        public String firstname { get; set; }

        [Required]
        public String lastname { get; set; }

        [Required]
        public String position { get; set; }

        public String image { get; set; }



    }
}