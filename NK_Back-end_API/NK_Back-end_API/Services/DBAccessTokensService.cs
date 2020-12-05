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
        private DB_DevEntities db = new DB_DevEntities();
        public string GenerateAccessTokens(string email, int minte = 60)
        {
            try
            {
                var memberItem = this.db.Member.SingleOrDefault(m => m.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found Member.");

                var accessTokensCreate = new AccessTokens
                {
                    token = Guid.NewGuid().ToString(),
                    exprise = DateTime.Now.AddMinutes(minte),
                    memberID = memberItem.id
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
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }

        }

        public Member VerifyAccessTokens(string accessTokens)
        {
            try
            {
                var accessTokenItem = this.db.AccessTokens.SingleOrDefault(item => item.token.Equals(accessTokens));
                if (accessTokenItem == null) return null;
                if (accessTokenItem.exprise < DateTime.Now) return null;
                return accessTokenItem.Member;
            }
            catch
            {
                return null;
            }
        }
    }
}