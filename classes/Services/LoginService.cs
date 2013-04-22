using BWA.bigWebDesk.Api.Models;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using System.Collections.Generic;
using System.Net;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/login")]
    public class Login : IReturn<LoginResponse>
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public string api_token { get; set; }
    }

    [Route("/organizations")]
    public class Organizations : ApiRequest {
    }


    public class LoginService : Service
    {
        public object Any(Login request)
        {
            var basicAuth = base.Request.GetBasicAuthUserAndPassword();
            string basicAuthEmail = "";
            string basicAuthPassword = "";
            if (basicAuth != null)
            {
                basicAuthEmail = basicAuth.Value.Key;
                basicAuthPassword = basicAuth.Value.Value;
            }
            string userName = request.username ?? basicAuthEmail;
            string userPass = request.password ?? basicAuthPassword;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPass))
            {
                base.Response.AddHeader(HttpHeaders.WwwAuthenticate, "Basic realm=\"/login\"");
                throw new HttpError(HttpStatusCode.Forbidden, "Incorrect login/password.");
            }
            if (!ApiUser.ValidateStatic(userName, userPass)) throw new HttpError(HttpStatusCode.Forbidden, "Login or Password is not correct.");
            Micajah.Common.Bll.Providers.LoginProvider lp = new Micajah.Common.Bll.Providers.LoginProvider();
            string api_token = lp.GetToken(userName);
            if (string.IsNullOrEmpty(api_token))
                throw new HttpError(HttpStatusCode.Forbidden, "User is not correct or inactive.");
            var hdUser = new ApiUser(api_token);
            return new LoginResponse { api_token = api_token };
        }

        [Secure()]
        public object Any(Organizations request)
        {
            Micajah.Common.Bll.Providers.LoginProvider lp = new Micajah.Common.Bll.Providers.LoginProvider();
            ApiUser hdUser = new ApiUser(request.api_token);
            Micajah.Common.Bll.OrganizationCollection orgsMc = lp.GetOrganizationsByLoginId(hdUser.LoginId);
            List<Organization> orgs = new List<Organization>(orgsMc.Count);
            foreach (Micajah.Common.Bll.Organization orgMc in orgsMc)
                try
                {
                    orgs.Add(new Organization(orgMc, lp.GetLoginInstances(hdUser.LoginId, orgMc.OrganizationId)));
                }
                catch
                { }
            return orgs;
        }
    }
}