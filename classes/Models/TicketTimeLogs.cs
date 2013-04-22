using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;

namespace BWA.bigWebDesk.Api.Models
{
    [DataContract(Name = "project_ticket_time_logs")]
    public class ProjectTicketTimeLogs : ModelItemCollectionGeneric<TimeLog>
    {
        public ProjectTicketTimeLogs(DataTable TicketTimeLogsTable) : base(TicketTimeLogsTable) { }

        public static List<TimeLog> GetProjectTicketTimeLogs(Guid organizationId, int departmentId, int projectId, int accountId, int page, int limit)
        {
            ProjectTicketTimeLogs _projectTicketTimeLogs = new ProjectTicketTimeLogs(bigWebApps.bigWebDesk.Data.Tickets.SelectProjectTimeLogs(organizationId, departmentId, DateTime.MinValue, projectId,
                0, accountId, 0));

            List<TimeLog> tlist = _projectTicketTimeLogs.Skip(page * limit).Take(limit).ToList();

            foreach (TimeLog tl in tlist)
            {
                tl.TicketKey = Models.Ticket.GetPseudoId(organizationId, departmentId, tl.TicketID);
            }

            return tlist;
        }
    }

    [DataContract(Name = "ticket_time_logs")]
    public class TicketTimeLogs : ModelItemCollectionGeneric<TimeLog>
    {
        public TicketTimeLogs(DataTable TicketTimeLogsTable) : base(TicketTimeLogsTable) { }

        public static List<TimeLog> GetTicketTimeLogs(Guid organizationId, int departmentId, int ticketId, string ticket_key, int page, int limit)
        {
            TicketTimeLogs _ticketTimeLogs = new TicketTimeLogs(bigWebApps.bigWebDesk.Data.Tickets.SelectTimes(organizationId, departmentId, ticketId));

            List<TimeLog> tlist = _ticketTimeLogs.Skip(page * limit).Take(limit).ToList();

            foreach (TimeLog tl in tlist)
            {
                tl.TicketKey = ticket_key;
            }

            return tlist;
        }
    }


    [DataContract(Name = "project_time_logs")]
    public class ProjectTimeLogs : ModelItemCollectionGeneric<ProjectTimeLog>
    {
        public ProjectTimeLogs(DataTable ProjectTimeLogsTable) : base(ProjectTimeLogsTable) { }

        public static List<ProjectTimeLog> GetProjectTimeLog(Guid organizationId, int departmentId, int projectId, int accountId, int page, int limit)
        {
            ProjectTimeLogs _projectTimeLog = new ProjectTimeLogs(bigWebApps.bigWebDesk.Data.Project.SelectProjectTimeList(organizationId, departmentId, projectId, DateTime.MinValue, 0,
                accountId, 0));

            List<ProjectTimeLog> tlist = _projectTimeLog.Skip(page * limit).Take(limit).ToList();
 
            return tlist;
        }
    }

    [DataContract(Name = "common_time_logs")]
    public class CommonTimeLogs : ModelItemCollectionGeneric<CommonTimeLog>
    {
        public CommonTimeLogs(DataTable CommonTimeLogsTable) : base(CommonTimeLogsTable) { }

        public static List<CommonTimeLog> GetCommonTimeLog(Guid organizationId, int departmentId, string type, int accountID,
            int projectID, int techID, int page, int limit)
        {
            int linkedFB = -1;
            int invoiced = -1;
            switch (type)
            {
                case "linked_fb":
                    linkedFB = 1;
                    break;
                case "unlinked_fb":
                    linkedFB = 0;
                    break;
                case "invoiced":
                    invoiced = 1;
                    break;
                case "not_invoiced":
                    invoiced = 0;
                    break;
            }
            CommonTimeLogs _commonTimeLog = new CommonTimeLogs(bigWebApps.bigWebDesk.Data.Tickets.SelectTop100MostTimeLogs(organizationId, departmentId, linkedFB, invoiced, accountID, projectID, techID));

            List<CommonTimeLog> clist = _commonTimeLog.Skip(page * limit).Take(limit).ToList();

            return clist;
        }
    }
}