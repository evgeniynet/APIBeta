using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;
using ServiceStack.Common.Web;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Projects")]
    public class Projects : ModelItemCollectionGeneric<Project>
    {
        public Projects(DataTable ProjectsTable) : base(ProjectsTable) { }

        public static List<Project> GetProjects(Guid organizationId, int departmentId, int accountId, int userId, bool isAccountManager, int page, int limit)
        {
            Projects _Projects = new Projects(bigWebApps.bigWebDesk.Data.Project.SelectListWithHours(organizationId, departmentId, accountId, 1, userId, false, isAccountManager));
            return _Projects.Skip(page * limit).Take(limit).ToList();
        }

        public static ProjectDetail GetProjectDetails(Guid OrgId, int DeptId, int projectId)
        {
            DataRow rowProjectDetail = bigWebApps.bigWebDesk.Data.Project.SelectDetail(OrgId, DeptId, projectId);
            if (rowProjectDetail == null)
                throw new HttpError(System.Net.HttpStatusCode.NotFound, "Wrong Project Id");
            return new ProjectDetail(rowProjectDetail, projectId);
        }
    }
}