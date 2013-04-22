using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using System.Data;
using BWA.bigWebDesk.Api.Models;

namespace BWA.bigWebDesk.Api
{
    public class PagedApiRequest : ApiRequest
    {
        private int _limit = 25;

        public string query { get; set; }
        public int limit
        {
            get { if (_limit <= 0) _limit = 25; return _limit; }
            set { _limit = value; }
        }
        public int page { get; set; }
    }

    /// <summary>
    /// Summary description for ApiRequest
    /// </summary>
    public class ApiRequest
    {
        public ApiRequest()
        {
        }

        protected internal ApiUser ApiUser { get; set; }
        protected internal string api_token { get; set; }
    }

    public class SecureAttribute : RequestFilterAttribute
    {
        public SecureAttribute()
        {
            ClientType = "";
        }

        public SecureAttribute(string clientType)
        {
            ClientType = clientType;
        }

        public string ClientType { get; set; }

        public override void Execute(IHttpRequest httpReq, IHttpResponse httpResp, object request)
        {
            var basicAuth = httpReq.GetBasicAuthUserAndPassword();
            ApiRequest apiRequest = request as ApiRequest;
            if (apiRequest == null)
            {
                //Custom Auth needed
                return;
            }

            string api_token = "";

            if (basicAuth == null)
            {
                api_token = httpReq.QueryString["api_token"];

                if (string.IsNullOrEmpty(api_token))
                {
                    httpResp.AddHeader(HttpHeaders.WwwAuthenticate, "Basic realm=\"/login\"");
                    throw new HttpError(HttpStatusCode.Unauthorized, "Invalid BasicAuth credentials");
                }
                else if (api_token.Length != 32)
                    throw new HttpError(HttpStatusCode.Forbidden, "Token is not correct.");
            }
            else
            {
                string key = basicAuth.Value.Key;
                string password = basicAuth.Value.Value;
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(password))
                    throw new HttpError(HttpStatusCode.Forbidden, "Token is not correct.");
                if (key == "x")
                {
                    if (password.Length != 32)
                        throw new HttpError(HttpStatusCode.Forbidden, "Token is not correct.");
                    apiRequest.api_token = password;
                }
                else
                {
                    apiRequest.ApiUser = ApiUser.getUser(httpReq);
                }
            }
        }
    }
}