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
        private IAccessTokensService AccessTokens;

        protected AccountController()
        {
            this.Account = new AccountService();
            //เลือกว่าจะเป็น แบบ DB หรือ JWT
            //this.AccessTokens = new DBAccessTokensService();
            this.AccessTokens = new JWTAccessTokensService();
        }


        //การลงทะเบียน
        [Route("api/account/register")]
        public IHttpActionResult PostRegister([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    model.password = PasswordHashModel.Hash(model.password);
                    this.Account.Register(model);
                    //return Ok("Successful"); 
                    return Ok(model);
                    //return Json(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Excption", ex.Message);
                }


            }

            return BadRequest(ModelState.GetErrorModelsState());

        }

        //เข้าสู่ระบบ 
        [Route("api/account/login")]
        public AccessTokenModel PostLogin([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (this.Account.Login(model) == true)
                    {
                        return new AccessTokenModel
                        {
                            accessToken = this.AccessTokens.GenerateAccessTokens(model.email)
                        };
                    }
                    throw new Exception("Username or Password is Valid.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }

            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelsState() }
                ));

        }
    }
}



//ตัวอย่าง เช็ค data
//public IHttpActionResult PostLogin([FromBody] LoginModel model)
//{
//    if (ModelState.IsValid)
//    {
//        return Json(model);
//    }
//    return BadRequest(ModelState.GetErrorModelsState());

//}