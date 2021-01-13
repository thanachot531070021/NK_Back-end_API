using NK_Back_end_API.Entitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Models
{
    public class CreateMemberModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public RoleAccount role { get; set; }
        public string image { get; set; }


    }
}