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
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        //[EmailAddress(ErrorMessage = "กรุณากรอกข้อมูลให้ผยู่ในรุปแบบ Email")]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        //[RegularExpression("/^[0 - 9]{6,10}$")]
        public string password { get; set; }
        [Required]
        //[Compare("password", ErrorMessage = "รหัสผ่านกับยืนยันรหัสผ่านต้องตรงกัน")]
        [Compare("password")]
        public string cpassword { get; set; }


       

    }
}