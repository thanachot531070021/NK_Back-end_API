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
        GetMemberModel GetMembers(MemberFilterOptions filters);
        void UpdatePrifile(string email, ProfileModel model);
        void ChangePassword(string email,ChangePasswordModel model);
        void CreateMember(CreateMemberModel model);

        void DeleteMember(int  id);

        void UpdateMember(int id, UpdateMemberModel model);


    }
}
