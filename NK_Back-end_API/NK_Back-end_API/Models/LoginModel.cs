using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public String email { get; set; }

        //[RegularExpression("/^[0 - 9]{6,10}$")]
        [Required]
        public String password { get; set; }

        public Boolean remember { get; set; }

    }

    public class AccessTokenModel{
        public String accessToken { get; set; }
    }
}