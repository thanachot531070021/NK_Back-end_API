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

        // ข้อมูลสมาชิก
        public IEnumerable<Member> MemberItem => this.db.Member.ToList();

        // เปลี่ยนรหัสผ่าน
        public void ChangePassword(string email, ChangePasswordModel model)
        {
            try
            {
                var memberItem = this.db.Member.SingleOrDefault(item => item.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                if (!PasswordHashModel.Verify(model.old_pass, memberItem.password))
                    throw new Exception("the old password is invalid");
                this.db.Member.Attach(memberItem);
                memberItem.password = PasswordHashModel.Hash(model.new_pass);
                memberItem.updated = DateTime.Now;
                this.db.Entry(memberItem).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        // แสดงรายการสมาชิก pagination และ Filter
        public GetMemberModel GetMembers(MemberFilterOptions filters)
        {
            var items = this.MemberItem.Select(m => new GetMembers
            {
                id = m.id,
                firstname = m.firstname,
                lastname = m.lastname,
                email = m.email,
                position = m.position,
                role = m.role,
                updated = m.updated
            });

            var memberItem= new GetMemberModel { 
                items = items
                        .Skip((filters.startPage - 1)* filters.limitPage)  /*Skip ตรวจสอบไปหน้าไหน*/
                        .Take(filters.limitPage) /*Takeเราจะเเสดงข้อมูลเท่าไหร่*/
                        .ToArray(),
                totalItems = items.Count()
            
            };
            //หากว่ามีการค้นหาข้อมูลในระบบ
            if (!string.IsNullOrEmpty(filters.searchType) && !string.IsNullOrEmpty(filters.searchText)) {

                string searchText = filters.searchText.ToLower();
                //ตรวจสอบว่า Type เป็นrole หรือป่าว
                if (filters.searchType.Equals("role")) 
                {
                    searchText = Enum.Parse(typeof(IRoleAccount), searchText).ToString().ToLower();
                }

                IEnumerable<GetMembers> searchItem = from m in items
                                         where m.GetType()  /*where m.email กำหนดค่าcol นั้นๆตรงๆ*/
                                         .GetProperty(filters.searchType)
                                         .GetValue(m)
                                         .ToString()
                                         .ToLower()
                                         .Contains(searchText)
                                         select m;

                memberItem.items = searchItem
                                    .Skip((filters.startPage - 1) * filters.limitPage)  /*Skip ตรวจสอบไปหน้าไหน*/
                                    .Take(filters.limitPage) /*Takeเราจะเเสดงข้อมูลเท่าไหร่*/
                                       .ToArray();
                memberItem.totalItems = searchItem.Count();
            }
            return memberItem;
        }

        //แก้ไขข้อมูลทั้งหมด
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