using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace NK_Back_end_API.Models
{
    public class RegisterModel
    {
        [Required]
        public String firstname { get; set; }

        [Required]
        public String lastname { get; set; }

        [Required]
        //[EmailAddress(ErrorMessage = "กรุณากรอกข้อมูลให้ผยู่ในรุปแบบ Email")]
        [EmailAddress]
        public String email { get; set; }

        //[RegularExpression("/^[0 - 9]{6,10}$")]
        [Required]
        public String password { get; set; }

        [Required]
        //[Compare("password", ErrorMessage = "รหัสผ่านกับยืนยันรหัสผ่านต้องตรงกัน")]
        [Compare("password")]
        public String cpassword { get; set; }


       

    }
}