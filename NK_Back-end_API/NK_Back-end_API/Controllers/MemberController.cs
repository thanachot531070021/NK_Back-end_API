using NK_Back_end_API.Entitiy;
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
        [Route("api/member/data")]
        //ตัว Member ข้างล่างมาจาก model ถ้าไม่อยากใช้ password สามารถไปสร้าง modelให้แล้วใช้ได้ ควรสร้างให้
        public Member GetMemberLogin()
        {
            var userLogin = User as UserLogin;
            //return userLogin.Member;  เอาเช็คข้อมูล
            return new Member
            {
                id = userLogin.Member.id,
                firstname = userLogin.Member.firstname,
                lastname = userLogin.Member.lastname,
                email = userLogin.Member.email,
                position = userLogin.Member.position,
                role = userLogin.Member.role,
                image = userLogin.Member.image,
                created = userLogin.Member.created,
                updated = userLogin.Member.updated
                //ใช้model Member แล้วตัว ที่ไม่ได้กำหนดค่าจะเป็น null ควรสร้างmodelใหม่ 
            };
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