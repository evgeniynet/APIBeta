using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/ping")]
    public class Ping {}

    public class PingService : Service
    {
        public object Any(Ping ping)
        {
            return "All OK!";
        }
    }
}