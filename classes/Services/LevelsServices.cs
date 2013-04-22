using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    
    [Route("/levels")]
    public class Levels : ApiRequest
    {
    }

    public class LevelsService : Service
    {
        [Secure()]
        public object Get(Levels request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Levels.UserLevels(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId);
        }
    }
}