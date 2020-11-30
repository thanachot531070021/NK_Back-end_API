using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
namespace NK_Back_end_API.Services
{
    public class AccountService : IAccountService
    {
        private DB_DevEntities db = new DB_DevEntities();


        // เข้าสู่ระบบ
        public bool Login(LoginModel model)
        {
            try{
                var memberItem = this.db.Member.SingleOrDefault(m=> m.email.Equals(model.email));
                if (memberItem != null) {
                    return PasswordHashModel.Verify(model.password, memberItem.password);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }

        }

        // ลงทะเบียน
        public void Register(RegisterModel model)
        {
            try
            {
                this.db.Member.Add(new Member
                {
                    firstname = model.lastname,
                    lastname = model.lastname,
                    email = model.email,
                    password = model.password,
                    position="55555",
                    image=null,
                    role=1,
                    created=DateTime.Now,
                    updated= DateTime.Now


                });

                this.db.SaveChanges();
            }

            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }

           
        }
    }
}



//catch เอาไว้เชค col ว่า type ผิดไหม
//catch (DbEntityValidationException e)
//{
//    foreach (var eve in e.EntityValidationErrors)
//    {
//        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//            eve.Entry.Entity.GetType().Name, eve.Entry.State);
//        foreach (var ve in eve.ValidationErrors)
//        {
//            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                ve.PropertyName, ve.ErrorMessage);

//        }
//    }
//    throw;
//}