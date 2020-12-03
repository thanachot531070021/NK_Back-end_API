using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK_Back_end_API.Interfaces
{
    interface IAccessTokensService
    {
        string GenerateAccessTokens(string email,int minte=60);
    }
}
