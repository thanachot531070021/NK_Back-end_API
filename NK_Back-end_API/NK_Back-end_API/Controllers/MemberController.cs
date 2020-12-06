using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Models;
using NK_Back_end_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static NK_Back_end_API.AuthenticationHandler;

namespace NK_Back_end_API.Controllers
{
    [Authorize]
    public class MemberController : ApiController
    {
        private IMemberService memberService;
        private MemberController()
        {
            this.memberService = new MemberService();
        }


        //แสดงข้อมูลผู้ใช้ที่ login
        [Route("api/member/data")]
        //ตัว Member ข้างล่างมาจาก model ถ้าไม่อยากใช้ password สามารถไปสร้าง modelให้แล้วใช้ได้ ควรสร้างให้
        public MemberModel GetMemberLogin()
        {
            var userLogin = this.memberService
                .MemberItem.SingleOrDefault(item => item.email.Equals(User.Identity.Name));
            //return userLogin.Member;  เอาเช็คข้อมูล
            if (userLogin == null) return null;
            return new MemberModel
            {
                id = userLogin.id,
                firstname = userLogin.firstname,
                lastname = userLogin.lastname,
                email = userLogin.email,
                position = userLogin.position,
                role = userLogin.role,
                image_type = userLogin.image_type,
                image_byte = userLogin.image,
                created = userLogin.created,
                updated = userLogin.updated
                //ใช้model Member แล้วตัว ที่ไม่ได้กำหนดค่าจะเป็น null ควรสร้างmodelใหม่ 
            };
        }



        //บันทึกข้อมูลโปรไฟล์
        [HttpPost]
        [Route("api/member/profile")]
        public IHttpActionResult UpdateProfile([FromBody] ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                try {
                    this.memberService.UpdatePrifile(User.Identity.Name, model);                  

                    return Ok(this.GetMemberLogin());
                }
                catch (Exception ex) {
                    ModelState.AddModelError("Exception",ex.Message);
                }
               
            }   
            return BadRequest(ModelState.GetErrorModelsState());
        }
    }
}



//ตัวอย่างก่อนส่งค่าไปให้ front End
//public class MemberController : ApiController
//{
//    [Route("api/member/data")]
//    public IHttpActionResult GetMemberLogin()
//    {
//        return Json(new
//        {
//            isLogin = User.Identity.IsAuthenticated,
//            EmailLogin = User.Identity.Name

//        });
//    }
//}