using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk.Data;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Ticket
    /// </summary>
    [DataContract(Name = "Worklist_Tickets")]
    public class WorklistTickets : ModelItemCollectionGeneric<WorklistTicket>
    {
        public WorklistTickets(DataTable TicketsTable) : base (TicketsTable){}
        
        public string OrgKey { get; set; }
        public string InstKey { get; set; }

        public static List<WorklistTicket> GetTickets(Guid OrgId, int DeptId, int UserId, string statusMode, string status, string role, bool IsTechAdmin, bool IsUseWorkDaysTimer, int page, int limit)
        {
            DataTable tickets = null;
            Worklist.QueryFilter _qFilter = new Worklist.QueryFilter();

            if (!string.IsNullOrEmpty(statusMode))
            {
                Worklist.TicketStatusMode ticketStatusMode;
                if (Enum.TryParse<Worklist.TicketStatusMode>(statusMode, true, out ticketStatusMode))
                {
                    _qFilter.TicketStatus = ticketStatusMode;
                }

                Worklist.SortMode ticketSortMode;
                if (Enum.TryParse<Worklist.SortMode>(role, true, out ticketSortMode))
                {
                    _qFilter.Sort = ticketSortMode;
                }
                int queueId;
                if (int.TryParse(status, out queueId))
                    _qFilter.TechnicianId = queueId;
                tickets = Worklist.SelectTicketsByFilter(OrgId, DeptId, UserId, _qFilter, IsTechAdmin);
            }
            else
            {
                role = role ?? "";
                bool isUser = role.Contains("user");
                bool isAltTech = role.Contains("alt_tech");
                role = isAltTech ? role.Replace("alt_tech", "") : role;
                bool isTech = role.Contains("tech");

                status = status == null ? "" : status.Replace("_", "");
                tickets = Worklist.SelectTicketsByFilter(OrgId, DeptId, UserId, isUser, isTech, isAltTech, status, IsTechAdmin);
            }
            return (new WorklistTickets(tickets)).Skip(page * limit).Take(limit).ToList();
        }
    }
}