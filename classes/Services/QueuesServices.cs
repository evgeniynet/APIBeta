using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/queues")]
    public class Queues : ApiRequest
    {
        public int? id { get; set; }
    }

    [Route("/queues/{id}")]
    public class Queue_Tickets : PagedApiRequest
    {
        public int id { get; set; }
    }

    public class QueuesService : Service
    {
        [Secure()]
        public object Get(Queues request)
        {
            ApiUser hdUser = request.ApiUser;
            return UnassignedQueues.TechQueues(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId);
        }

        [Secure()]
        public object Get(Queue_Tickets request)
        {
            ApiUser hdUser = request.ApiUser;
            return WorklistTickets.GetTickets(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId, "allopen", request.id.ToString(), "TechTickets", hdUser.IsTechAdmin, hdUser.IsUseWorkDaysTimer, request.page, request.limit);
        }
    }
}