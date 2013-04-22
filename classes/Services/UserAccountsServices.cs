using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/technicians")]
    public class Technicians : PagedApiRequest  {}

    public class TechniciansService : Service
    {
        [Secure()]
        public object Any(Technicians request)
        {
            ApiUser hdUser = request.ApiUser;
            return UserAccounts.Technicians(hdUser.OrganizationId, hdUser.DepartmentId, request.page, request.limit);
        }
    }

    [Route("/users")]
    [Route("/users/{id}")]
    public class Users : PagedApiRequest
    {
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public int id { get; set; }
    }

    public class UsersService : Service
    {        
        [Secure()]
        public object Any(Users request)
        {
            ApiUser hdUser = request.ApiUser;
            if (request.id > 0)
                return UserAccounts.GetUser(hdUser.OrganizationId, hdUser.DepartmentId, request.id);
            return UserAccounts.FindUsers(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId, request.firstname, request.lastname, request.email, request.page, request.limit);
        }
    }
}