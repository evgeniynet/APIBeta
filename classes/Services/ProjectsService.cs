using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using BWA.bigWebDesk.Api.Models;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/projects/{id}")]
    public class Projects : ApiRequest
    {
        public int id { get; set; }
    }

    [Route("/projects")]
    public class Projects_List : PagedApiRequest
    {
    }

    public class ProjectsService : Service
    {
        [Secure()]
        public object Get(Projects request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Projects.GetProjectDetails(hdUser.OrganizationId, hdUser.DepartmentId, request.id);
        }

        [Secure()]
        public object Get(Projects_List request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);
            return Models.Projects.GetProjects(hdUser.OrganizationId, hdUser.DepartmentId, 0, hdUser.IsTechAdmin ? 0 : hdUser.UserId, instanceConfig.AccountManager, request.page, request.limit);
        }
    }
}