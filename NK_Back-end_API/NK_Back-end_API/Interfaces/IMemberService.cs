using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK_Back_end_API.Interfaces
{
    interface IMemberService
    {
        IEnumerable<Member> MemberItem { get; }
        void UpdatePrifile(string email, ProfileModel model);
    }
}
