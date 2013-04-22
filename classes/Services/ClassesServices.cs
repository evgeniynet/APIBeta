using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    [Secure()]
    [Route("/classes")]
    public class Classes : ApiRequest
    {
    }

    public class ClassesService : Service
    {
        [Secure()]
        public object Get(Classes request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Classes.UserClasses(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId);
        }
    }
}