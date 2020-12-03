using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NK_Back_end_API.Models;

namespace NK_Back_end_API.Services
{
    public class DBAccessTokensService : IAccessTokensService
    {
        private Database_Entities db = new Database_Entities();
        public string GenerateAccessTokens(string email, int minte = 60)
        {
            try {
                var memberItem = this.db.Member.SingleOrDefault(m => m.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found Member.");

                var accessTokensCreate = new AccessTokens {
                    token = Guid.NewGuid().ToString(),
                    exprise = DateTime.Now.AddMinutes(minte),
                    memberID=memberItem.id
                };
                this.db.AccessTokens.Add(accessTokensCreate);
                //this.db.SaveChanges();
                if (this.db.SaveChanges() > 0)
                {
                    return accessTokensCreate.token;
                }
                else
                {
                    throw new Exception("Not saved Member.");
                }

            }
            catch (Exception ex) {
                throw ex.GetErrorException();
            }

        }
    }
}