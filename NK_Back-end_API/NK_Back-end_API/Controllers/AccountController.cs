using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NK_Back_end_API.Controllers
{
    public class AccountController : ApiController
    {
        //การลงทะเบียน

        [Route("api/account/register")]
        public IHttpActionResult PostRegister([FromBody] RegisterModel model) 
        {
            if (ModelState.IsValid)
            {
                model.password = PasswordHashModel.Hash(model.password);
                return Json(model);

            }

            return BadRequest(ModelState.GetErrorModelsState());

        }
    }
}
