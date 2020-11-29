using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Models;
using NK_Back_end_API.Services;
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

        private IAccountService Account;

        protected AccountController()
        {
            this.Account = new AccountService();
        }


        //การลงทะเบียน
        [Route("api/account/register")]
        public IHttpActionResult PostRegister([FromBody] RegisterModel model) 
        {
            if (ModelState.IsValid)
            {

                try {
                    model.password = PasswordHashModel.Hash(model.password);
                    this.Account.Register(model);
                    //return Ok("Successful"); 
                    return Ok(model);
                    //return Json(model);
                }
                catch (Exception ex){
                    ModelState.AddModelError("Excption", ex.Message);
                }
             

            }

            return BadRequest(ModelState.GetErrorModelsState());

        }
    }
}
