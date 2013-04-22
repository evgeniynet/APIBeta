using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using ServiceStack.Common.Web;
using System.Net;
using System;

namespace BWA.bigWebDesk.Api.Services
{
    
    //[Route("/todos")]
    [Route("/tickets/{key}/todos")]
    [Route("/projects/{id}/todos")]
    public class Todos_List : ApiRequest
    {
        public string key { get; set; }
        public int? id { get; set; }
    }

    [Route("/todos/{id}")]
    public class Todos : ApiRequest
    {
        public string id { get; set; }
    }


    public class TodosService : Service
    {
        [Secure()]
        public object Get(Todos_List request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Todos.GetTicketTodos(hdUser.OrganizationId, hdUser.DepartmentId, string.IsNullOrEmpty(request.key) ?
                                                                      0 : BWA.bigWebDesk.Api.Models.Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key),
                                                                      request.id ?? 0);
        }

        [Secure()]
        public object Delete(Todos request)
        {
            ApiUser hdUser = request.ApiUser;
            Guid todoId;
            if (Guid.TryParse(request.id, out todoId))
            {
                bigWebApps.bigWebDesk.Data.ToDo.DeleteToDoItem(hdUser.OrganizationId, hdUser.DepartmentId, todoId.ToString());
                return new HttpResult("", HttpStatusCode.OK);
            }
            return new HttpResult("", HttpStatusCode.NotFound);
        }
    }
}