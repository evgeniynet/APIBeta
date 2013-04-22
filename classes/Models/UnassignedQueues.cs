using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Unassigned_Queues")]
    public class UnassignedQueues : ModelItemCollectionGeneric<UnassignedQueue>
    {
        public UnassignedQueues(DataTable QueuesTable) : base(QueuesTable) { }

        public static List<UnassignedQueue> TechQueues(Guid OrgId, int DeptId, int UserId)
        {
            //string _query = @"SELECT J.id AS Id, Max(J.QueEmailAddress) AS QueEmailAddress, Max(L.FirstName+' '+L.LastName) AS FullName, SUM(CASE WHEN T.Status<>'Closed' THEN 1 ELSE 0 END) as TicketsCount FROM tbl_LoginCompanyJunc J INNER JOIN tbl_Logins L ON J.login_id = L.id LEFT OUTER JOIN tbl_ticket T ON T.company_id=" + DeptId.ToString() + " AND T.Technician_Id=J.id LEFT OUTER JOIN QueueMembers QM ON QM.DepartmentId=" + DeptId.ToString() + " AND QM.QueueId=J.id WHERE J.company_id = " + DeptId.ToString() + " AND J.UserType_id = 4 AND QM.UserId=" + UserId.ToString() + " GROUP BY J.id ORDER BY FullName";
            string _query = @"SELECT J.id AS Id, Max(J.QueEmailAddress) AS QueEmailAddress, Max(L.FirstName+' '+L.LastName) AS FullName, SUM(CASE WHEN T.Status<>'Closed' THEN 1 ELSE 0 END) as TicketsCount FROM tbl_LoginCompanyJunc J INNER JOIN tbl_Logins L ON J.login_id = L.id LEFT OUTER JOIN tbl_ticket T ON T.company_id=" + DeptId.ToString() + " AND T.Technician_Id=J.id WHERE J.company_id = " + DeptId.ToString() + " AND J.UserType_id = 4 GROUP BY J.id ORDER BY FullName";
            UnassignedQueues _queues = new UnassignedQueues(bigWebApps.bigWebDesk.Data.UnassignedQueues.SelectByQuery(_query, OrgId));
            return _queues.List;
        }
    }
}