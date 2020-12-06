using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NK_Back_end_API.Services
{

    public class MemberService : IMemberService
    {
        private DB_DevEntities db = new DB_DevEntities();

        public IEnumerable<Member> MemberItem => this.db.Member.ToList();

        public void UpdatePrifile(string email, ProfileModel model)
        {
            try
            {
                var memberItem = this.db.Member.SingleOrDefault(item => item.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                this.db.Member.Attach(memberItem);
                memberItem.firstname = model.firstname;
                memberItem.lastname = model.lastname;
                memberItem.position = model.position;
                memberItem.updated = DateTime.Now;

                //ตรวจสอบว่ามีการอัพโหลดหรือไม่
                if (!string.IsNullOrEmpty(model.image))
                {
                    string[] images = model.image.Split(',');
                    if (images.Length == 2)
                    {
                        if (images[0].IndexOf("image") >= 0)
                        {
                            memberItem.image_type = images[0];
                            memberItem.image = Convert.FromBase64String(images[1]);
                        }
                    }
                    
                }


                this.db.Entry(memberItem).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }

        }
    }
}