using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using BWA.bigWebDesk.Api.Models;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using bigWebApps.bigWebDesk;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.ServiceInterface.Cors;
using System.Data;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/tickets/", "GET, OPTIONS")]
    public class Tickets_List : PagedApiRequest
    {
        public string status { get; set; }
        public string role { get; set; }
    }

    [Route("/tickets/{key}", "GET, DELETE, OPTIONS")]
    public class Tickets : ApiRequest
    {
        public string key { get; set; }
    }


    [Route("/tickets/counts")]
    [Route("/tickets/counts/{status}")]
    public class Tickets_Counts : ApiRequest
    {
        public string status { get; set; }
    }

    [Route("/tickets/{key}","PUT, OPTIONS")]
    [Route("/tickets/{key}/technicians/{tech_id}", "PUT, POST, OPTIONS")]
    [Route("/tickets/{key}/{action}", "PUT, OPTIONS")]
    [Route("/tickets/{key}/users/{user_id}", "POST, OPTIONS")]
    public class Ticket_Actions : ApiRequest
    {
        public string key { get; set; }
        public string status { get; set; }
        public string action { get; set; }
        public string note_text { get; set; }
        public int tech_id { get; set; }
        public int user_id { get; set; }
        public int class_id { get; set; }
        public bool keep_attached { get; set; }
        public bool is_send_notifications { get; set; }
        public bool resolved { get; set; }
        public bool confirmed { get; set; }
        public string confirm_note { get; set; }
    }

    public class TicketsService : Service
    {
        [Secure()]
        public object Get(Tickets_List request)
        {
            ApiUser  hdUser = request.ApiUser;
            var tickets = WorklistTickets.GetTickets(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId, request.query, request.status, request.role, hdUser.IsTechAdmin, hdUser.IsUseWorkDaysTimer, request.page, request.limit);
            return tickets;
        }

        [Secure()]
        public object Get(Tickets request)
        {
            ApiUser hdUser = request.ApiUser;
            request.key = request.key ?? "";
            return new Ticket(hdUser.OrganizationId, hdUser.DepartmentId, request.key, hdUser.InstanceId);
        }

        [Secure()]
        public object Post(Ticket request)
        {
            ApiUser hdUser = ApiUser.getUser(base.Request);
            int _newTktId = Ticket.CreateNew(hdUser, request);
            if (_newTktId > 0)
            {
                if (request.Users != null)
                    foreach (TicketAssignee _ta in request.Users) Ticket.AttachAlternateUser(hdUser, _newTktId, _ta.UserId);

                if (request.Technicians != null)
                    foreach (TicketAssignee _ta in request.Technicians) Ticket.AttachAlternateTechnician(hdUser, _newTktId, _ta.UserId);

                return new HttpResult(new Ticket(hdUser.OrganizationId, hdUser.DepartmentId, _newTktId, hdUser.InstanceId)) { StatusCode = HttpStatusCode.Created };
            }

            CustomNames _cNames = CustomNames.GetCustomNames(hdUser.OrganizationId, hdUser.DepartmentId);
            string errMsg = "Ticket Not Saved. ";
            switch (_newTktId)
            {
                case -1:
                    errMsg += "Input level is not setup for this class.";
                    break;
                case -2:
                    errMsg += "No routing options are enabled. No route found. Must choose " + _cNames.Technician.AbbreviatedSingular + " specifically.";
                    break;
                case -3:
                    errMsg += "No Route Found. Routing configuration must be modified.";
                    break;
                case -4:
                    errMsg += "Level does not exists.";
                    break;
                case -5:
                    errMsg += "Route found but " + _cNames.Technician.AbbreviatedSingular + " could not be returned. Please check routing order for errors.";
                    break;
            }
            throw new HttpError(HttpStatusCode.NotFound, new ArgumentException(errMsg));
        }

        [Secure()]
        public object Delete(Tickets request)
        {
            ApiUser hdUser = request.ApiUser;
            bigWebApps.bigWebDesk.Data.Tickets.DeleteTicket(hdUser.OrganizationId, hdUser.DepartmentId, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key));
            return new HttpResult("", HttpStatusCode.OK);
        }

        [Secure()]
        public object Put(Ticket_Actions request)
        {
            ApiUser hdUser = request.ApiUser;

            request.note_text = request.note_text ?? "";
            request.status = request.status ?? "";
            request.status = request.status.Replace("_", "");

            Ticket.Status status;

            if (Enum.TryParse<Ticket.Status>(request.status, true, out status))
            {
                switch (status)
                {
                    case Ticket.Status.OnHold:
                        Ticket.OnHold(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text);
                        break;
                    case Ticket.Status.PartsOnOrder:
                        throw new HttpError(HttpStatusCode.NotFound, "Incorrect status");
                    case Ticket.Status.Closed:
                        Ticket.Close(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text, request.is_send_notifications, request.resolved, request.confirmed, request.confirm_note);
                        break;
                    case Ticket.Status.Open:
                        Ticket.ReOpen(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text);
                        break;
                }
            }
            else if (request.tech_id > 0)
            {
                Ticket.TransferToTech(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.tech_id, request.note_text, request.keep_attached);
            }
            else if (!string.IsNullOrEmpty(request.action))
            {
                switch (request.action.ToLower())
                {
                    case "pickup": Ticket.PickUp(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text);
                        break;
                    case "confirm": Ticket.Confirm(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.note_text);
                        break;
                }
            }
            return new HttpResult("", HttpStatusCode.OK);
        }

        [Secure()]
        public object Post(Ticket_Actions request)
        {
            ApiUser hdUser = request.ApiUser;

            if (request.tech_id > 0)
                Ticket.AttachAlternateTechnician(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.tech_id);
            else if (request.user_id > 0)
                Ticket.AttachAlternateUser(hdUser, Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), request.user_id);

            return new HttpResult("", HttpStatusCode.OK);
        }

        [Secure()]
        public object Get(Tickets_Counts request)
        {
            ApiUser hdUser = request.ApiUser;
            request.status = request.status ?? "";
               
            TicketCount tk = new TicketCount(bigWebApps.bigWebDesk.Data.Tickets.SelectTicketCounts(hdUser.OrganizationId, hdUser.DepartmentId, 1, hdUser.UserId));
            switch (request.status.ToLower())
            {
                case "new": return tk.New;
                case "open": return tk.Open;
                case "total": return tk.Total;
                case "onhold": return tk.OnHold;
                case "reminder": return tk.Reminder;
                case "parts_on_order": return tk.PartsOnOrder;
                case "unconfirmed": return tk.Unconfirmed;
                default: return tk;
            }
            return new HttpResult("", HttpStatusCode.OK);
        }
    }
}