using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{

    [Route("/task_types")]
    [Route("/tickets/{key}/task_types")]
    public class Task_Types : ApiRequest {
        public string key { get; set; }
    }

    public class TaskTypesService : Service
    {
        [Secure()]
        public object Any(Task_Types request)
        {
            ApiUser hdUser = request.ApiUser;
            if (!string.IsNullOrEmpty(request.key))
                return Models.TaskTypes.TicketAssignedTaskTypes(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key));
            return Models.TaskTypes.SelectAllTaskTypes(hdUser.OrganizationId, hdUser.DepartmentId);
        }
    }
}