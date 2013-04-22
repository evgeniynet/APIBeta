using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{

    [Route("/config")]
    public class Config : ApiRequest
    {
    }

    public class ConfigService : Service
    {
        [Secure()]
        public object Get(Config request)
        {
            return new Models.Instance_Config(request.ApiUser);
        }
    }
}