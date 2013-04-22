using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace BWA.bigWebDesk.Api.Services
{

    [Route("/articles/{key}")]
    public class Articles : ApiRequest
    {
        public string key { get; set; }
    }

    [Route("/articles")]
    public class Articles_List : PagedApiRequest
    {
    }

    public class ArticlesService : Service
    {
        [Secure()]
        public object Get(Articles request)
        {
            ApiUser hdUser = request.ApiUser;
            return  Models.Articles.GetArticle(hdUser.OrganizationId, hdUser.DepartmentId, BWA.bigWebDesk.Api.Models.Ticket.GetId(hdUser.OrganizationId, hdUser.DepartmentId, request.key), hdUser.InstanceId);
        }

        [Secure()]
        public object Get(Articles_List request)
        {
            ApiUser hdUser = request.ApiUser;
            return Models.Articles.GetArticles(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId, request.page, request.limit);
        }
    }
}