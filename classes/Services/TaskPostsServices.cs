using System;
using System.Collections.Generic;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using System.Net;
using ServiceStack.Common.Web;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/tickets/{key}/posts")]
    public class Posts : PagedApiRequest
    {
        public string key { get; set; }
        public string note_text { get; set; }
    }

    public class TaskPostsService : Service
    {        
        [Secure()]
        public object Get(Posts request)
        {
            ApiUser hdUser = request.ApiUser;
            return TicketLogRecords.TicketLog(hdUser.OrganizationId, hdUser.DepartmentId, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.key, request.page, request.limit);
        }
            
        [Secure()]
        public object Post(Posts request)
        {
            ApiUser hdUser = request.ApiUser;
            request.note_text = request.note_text ?? "";
            Ticket.Response(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text);
            return new HttpResult("", HttpStatusCode.OK);
        }}
}