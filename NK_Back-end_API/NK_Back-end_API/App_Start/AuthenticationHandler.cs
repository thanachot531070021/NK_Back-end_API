using NK_Back_end_API.Entitiy;
using NK_Back_end_API.Interfaces;
using NK_Back_end_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NK_Back_end_API
{

    //ใช้ HTTP Message Handlers in ASP.NET 
    public class AuthenticationHandler : DelegatingHandler
    {
        private IAccessTokensService accessTokensService;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var Authorization = request.Headers.Authorization;

            if (Authorization != null)
            {
                string AccessToken = Authorization.Parameter;
                string AccessTokenType = Authorization.Scheme;
                if (AccessTokenType.Equals("Bearer"))
                {
                    //เลือกว่าจะเป็น แบบ DB หรือ JWT
                    //this.accessTokensService = new DBAccessTokensService();
                    this.accessTokensService = new JWTAccessTokensService();

                    var MemberItem = this.accessTokensService.VerifyAccessTokens(AccessToken);
                    if (MemberItem != null) {
                        var userLogin = new UserLogin(new GenericIdentity(MemberItem.email), MemberItem.role);
                        userLogin.Member = MemberItem;
                        Thread.CurrentPrincipal = userLogin;
                        HttpContext.Current.User = userLogin;
                    }
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
            
        public class UserLogin : GenericPrincipal
        {
            public Member Member { get; set; }
            public UserLogin(IIdentity identity, RoleAccount roles) 
                : base(identity, new string[] { roles.ToString() })
            {
            }
        }
    }
}