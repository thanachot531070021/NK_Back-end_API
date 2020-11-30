using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK_Back_end_API.Interfaces
{
    interface IAccountService
    {
        void Register(RegisterModel model);
        Boolean Login(LoginModel model);  // ใช้ Boolean เพราะ ต้องการค่าที่ Return ออกมา ผ่าน/ไม่ผ่าน
    }
}
