using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api.Services
{
    
    [Route("/priorities")]
    public class Priorities : ApiRequest
    {
    }

    public class PrioritiesService : Service
    {
        [Secure()]
        public object Get(Priorities request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Priorities.All(hdUser.OrganizationId, hdUser.DepartmentId);
        }
    }
}