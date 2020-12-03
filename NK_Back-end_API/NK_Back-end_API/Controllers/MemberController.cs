using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NK_Back_end_API.Controllers
{
    public class MemberController : ApiController
    {
        [Route("api/member/data")]
        public IHttpActionResult GetMemberLogin() {
            return Json( new { 
            isLogin= User.Identity.IsAuthenticated,
            EmailLogin= User.Identity.Name

            });
        }
    }
}
