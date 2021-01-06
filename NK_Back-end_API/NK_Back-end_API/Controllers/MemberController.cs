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
                try
                {
                    this.memberService.UpdatePrifile(User.Identity.Name, model);

                    return Ok(this.GetMemberLogin());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }

            }
            return BadRequest(ModelState.GetErrorModelsState());
        }

        //เปลี่ยนรหัสผ่าน
        [Route("api/member/change-password")]
        public IHttpActionResult PostChangePassword([FromBody] ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try {
                    this.memberService.ChangePassword(User.Identity.Name, model);
                    return Ok(new {Message="Password has change." });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelsState());
        }

        //แสดงรายการสมาชิกทั้งหมด
        public  GetMemberModel GetMember([FromUri]MemberFilterOptions filters) {

            
            if (ModelState.IsValid) {
                return this.memberService.GetMembers(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelsState() }
                ));
        }


        //เพิ่มข้อมูลสมาชิก (จำลอง)
        [Route("api/member/generate")]
        public IHttpActionResult PostGenerateMember() {
            try {
                var memberItems = new List<Member>();
                var password = PasswordHashModel.Hash("123456");
                var positions = new string[] { "Backend developer", "Frontend developer" };
                var roles = new IRoleAccount[] { IRoleAccount.Admin, IRoleAccount.Employee, IRoleAccount.Member };
                var random = new Random();
                for (var index = 1; index <= 10; index++)
                {
                    memberItems.Add(new Member
                    {
                        email = $"mail-{index}@mail.com",
                        password = password,
                        firstname = $"Firstname{index}",
                        lastname = $"positions{index}",
                        position = positions[random.Next(0, 2)],
                        role = roles[random.Next(0, 3)],
                        created = DateTime.Now,
                        updated = DateTime.Now


                    });

                }
                var db = new DB_DevEntities();
                db.Member.AddRange(memberItems);
                db.SaveChanges();
                return Ok("Generate successful");
            }
            catch (Exception ex) {
                ModelState.AddModelError("Exception", ex);
                return BadRequest(ModelState.GetErrorModelsState());
            }
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