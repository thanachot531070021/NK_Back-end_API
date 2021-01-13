using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

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
                onConvertBase94ToImage(memberItem, model.image);


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
            if (!string.IsNullOrEmpty(filters.searchType) && filters.searchType.Equals("updated"))
            {
                var paramItem = HttpContext.Current.Request.Params;
                var fromDate = paramItem.Get("searchText[from]").Replace(" GMT+0700 (Indochina Time)", "");
                var toDate = paramItem.Get("searchText[to]").Replace(" GMT+0700 (Indochina Time)", "");

                filters.searchText = $"{fromDate},{toDate}";

            }
            var items = this.MemberItem.Select(m => new GetMembers
            {
                id = m.id,
                firstname = m.firstname,
                lastname = m.lastname,
                email = m.email,
                position = m.position,
                role = m.role,
                updated = m.updated
            })
                .OrderByDescending(m => m.updated);


            var memberItem = new GetMemberModel
            {
                items = items
                        .Skip((filters.startPage - 1) * filters.limitPage)  /*Skip ตรวจสอบไปหน้าไหน*/
                        .Take(filters.limitPage) /*Takeเราจะเเสดงข้อมูลเท่าไหร่*/
                        .ToArray(),
                totalItems = items.Count()

            };
            //หากว่ามีการค้นหาข้อมูลในระบบ
            if (!string.IsNullOrEmpty(filters.searchType) && !string.IsNullOrEmpty(filters.searchText))
            {

                string searchText = filters.searchText;
                string searchType = filters.searchType;

                IEnumerable<GetMembers> searchItem = new GetMembers[] { };


                //ตรวจสอบว่า Type เป็นrole หรือป่าว

                switch (searchType)
                {
                    //ค้นหาจากวันที่
                    case "updated":

                        var searchTexts = searchText.Split(',');

                        DateTime FromDate = DateTime.Parse(searchTexts[0], CultureInfo.InvariantCulture);
                        DateTime ToDate = DateTime.Parse(searchTexts[1], CultureInfo.InvariantCulture);

                        searchItem = from m in items
                                     where m.updated >= FromDate && m.updated <= ToDate
                                     select m;
                        break;



                    //ค้นหาสิทธ์ผู้ใช้งาน
                    case "role":
                        searchItem = from m in items
                                     where Convert.ToInt16(m.GetType()  /*where m.email กำหนดค่าcol นั้นๆตรงๆ*/
                                     .GetProperty(filters.searchType)
                                     .GetValue(m)) == Convert.ToInt16(searchText)
                                     select m;
                        break;


                    //ค้นหาทั่วไป
                    default:
                        searchItem = from m in items
                                     where m.GetType()  /*where m.email กำหนดค่าcol นั้นๆตรงๆ*/
                                     .GetProperty(filters.searchType)
                                     .GetValue(m)
                                     .ToString()
                                     .ToLower()
                                     .Contains(searchText.ToLower())
                                     select m;
                        break;
                }





                memberItem.items = searchItem
                                    .Skip((filters.startPage - 1) * filters.limitPage)  /*Skip ตรวจสอบไปหน้าไหน*/
                                    .Take(filters.limitPage) /*Takeเราจะเเสดงข้อมูลเท่าไหร่*/
                                       .ToArray();
                memberItem.totalItems = searchItem.Count();
            }
            return memberItem;
        }

        //สร้างข้อมูลสมาชิกใหม่
        public void CreateMember(CreateMemberModel model)
        {
            try
            {

                Member memberCreate = new Member
                {
                    firstname = model.lastname,
                    lastname = model.lastname,
                    email = model.email,
                    password = PasswordHashModel.Hash(model.password),
                    position = model.position,
                    role = IRoleAccount.Member,
                    created = DateTime.Now,
                    updated = DateTime.Now
                };

                this.onConvertBase94ToImage(memberCreate, model.image);
                this.db.Member.Add(memberCreate);
                this.db.SaveChanges();
            }

            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        //ลบข้อมูลสมาชิก
        public void DeleteMember(int id)
        {
            try
            {
                var memberDelete = this.MemberItem.SingleOrDefault(m => m.id == id);
                if (memberDelete == null) throw new Exception("Not Founf Member");
                this.db.Member.Remove(memberDelete);
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }


        }

        public void UpdateMember(int id, UpdateMemberModel model)
        {
            try
            {
                var memberUpdate = this.MemberItem.SingleOrDefault(m => m.id == id);
                var emailSame = this.MemberItem.SingleOrDefault(m => m.email== model.email && m.id!=id );
                if (emailSame!=null) throw new Exception("Have Email in Databbase");
                if (memberUpdate == null) throw new Exception("Not Found mrmber");
                this.db.Member.Attach(memberUpdate);
                memberUpdate.email = model.email;
                memberUpdate.firstname = model.firstname;
                memberUpdate.lastname = model.lastname;
                memberUpdate.position = model.position;
                memberUpdate.role = model.role;
                this.onConvertBase94ToImage(memberUpdate, model.image);
                memberUpdate.updated = DateTime.Now;
                if (!string.IsNullOrEmpty(model.password))
                {
                    memberUpdate.password = PasswordHashModel.Hash(model.password);
                }
                this.db.Entry(memberUpdate).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }


        // แปลง base64 Image เป็น Byte และชนิกของรูป
        private void onConvertBase94ToImage(Member memberItem, string image)
        {
            //ตรวจสอบว่ามีการอัพโหลดหรือไม่
            if (!string.IsNullOrEmpty(image))
            {
                string[] images = image.Split(',');
                if (images.Length == 2)
                {
                    if (images[0].IndexOf("image") >= 0)
                    {
                        memberItem.image_type = images[0];
                        memberItem.image = Convert.FromBase64String(images[1]);
                    }
                }

            }
            else if (image == null)
            {
                memberItem.image_type = null;
                memberItem.image = null;
            }
        }


    }
}