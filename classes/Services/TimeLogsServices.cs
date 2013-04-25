using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using ServiceStack.Common.Web;
using System.Net;
using System.Data;

namespace BWA.bigWebDesk.Api.Services
{

    [Route("/time", "GET, OPTIONS")]
    public class Project_Time_Logs : PagedApiRequest
    {
        public string ticket { get; set; }
        public int? project { get; set; }
        public string type { get; set; }
        public int? account { get; set; }
        public int? tech { get; set; }
    }

    [Route("/time/{key}", "GET, OPTIONS")]
    public class Time_Logs : ApiRequest
    {
        public string key { get; set; }
        public string type { get; set; }
    }

    [Route("/time", "POST, OPTIONS")]
    public class Tickets_Time_Logs : ApiRequest
    {
        public string ticket_key { get; set; }
        public int task_type_id { get; set; }
        public string note_text { get; set; }
        public decimal hours { get; set; }
    }


    public class TimeLogsService : Service
    {
        [Secure()]
        public object Get(Project_Time_Logs request)
        {
            ApiUser hdUser = request.ApiUser;
            if (!string.IsNullOrEmpty(request.ticket))
                return TicketTimeLogs.GetTicketTimeLogs(hdUser.OrganizationId, hdUser.DepartmentId, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.ticket), request.ticket, request.page, request.limit);

            if (!string.IsNullOrEmpty(request.type) &&
                ((request.type.ToLower() == "recent" || request.type.ToLower() == "linked_fb") || request.type.ToLower() == "unlinked_fb"
                || request.type.ToLower() == "invoiced" || request.type.ToLower() == "not_invoiced"))
            {
                int accountID = 0;
                int projectID = 0;
                int techID = 0;
                if (request.account.HasValue)
                {
                    accountID = request.account.Value;
                }
                if (request.project.HasValue)
                {
                    projectID = request.project.Value;
                }
                if (request.tech.HasValue)
                {
                    techID = request.tech.Value;
                }
                return Models.CommonTimeLogs.GetCommonTimeLog(hdUser.OrganizationId, hdUser.DepartmentId, request.type.ToLower(), accountID, projectID, techID, request.page, request.limit);
            }

            if (!request.project.HasValue)
                throw new HttpError(HttpStatusCode.NotFound, "Incorrect project id");

            ProjectDetail projectDetail = Models.Projects.GetProjectDetails(hdUser.OrganizationId, hdUser.DepartmentId, request.project.Value);
            if (!string.IsNullOrEmpty(request.type) && request.type.ToLower() == "tickets")
                return Models.ProjectTicketTimeLogs.GetProjectTicketTimeLogs(hdUser.OrganizationId, hdUser.DepartmentId, request.project.Value, projectDetail.AccountId, request.page, request.limit);
            return Models.ProjectTimeLogs.GetProjectTimeLog(hdUser.OrganizationId, hdUser.DepartmentId, request.project.Value, projectDetail.AccountId, request.page, request.limit);
        }

        [Secure()]
        public object Post(Tickets_Time_Logs request)
        {
            ApiUser hdUser = request.ApiUser;
            request.ticket_key = request.ticket_key ?? "";
            Ticket.InputTime(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.ticket_key), request.task_type_id, request.hours, hdUser.TimeZoneOffset, request.note_text);
            return new HttpResult("", HttpStatusCode.OK);
        }

        [Secure()]
        public object Get(Time_Logs request)
        {
            ApiUser hdUser = request.ApiUser;

            int logID = 0;
            if (!int.TryParse(request.key, out logID))
            {
                throw new HttpError(HttpStatusCode.NotFound, "incorrect key");
            }

            if (!string.IsNullOrEmpty(request.type) && request.type.ToLower() == "project")
            {
                DataRow plRow = bigWebApps.bigWebDesk.Data.Project.SelectProjectTimeByID(hdUser.OrganizationId, hdUser.DepartmentId, logID);
                if (plRow == null)
                {
                    throw new HttpError(HttpStatusCode.NotFound, "key not found");
                }
                return new ProjectTimeLog(plRow);
            }

            DataRow tlRow = bigWebApps.bigWebDesk.Data.Tickets.SelectTicketTimeByID(hdUser.OrganizationId, hdUser.DepartmentId, logID);
            if (tlRow == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, "key not found");
            }
            return new TimeLog(tlRow);
        }
    }
}