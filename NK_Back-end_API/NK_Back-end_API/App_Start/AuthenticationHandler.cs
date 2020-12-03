using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NK_Back_end_API
{

    //ใช้ HTTP Message Handlers in ASP.NET 
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var Authorization = request.Headers.Authorization;

            if (Authorization != null)
            {
                string AccessToken = Authorization.Parameter;
                string AccessTokenType = Authorization.Scheme;
                if (AccessTokenType.Equals("Brarer"))
                {

                }

            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}