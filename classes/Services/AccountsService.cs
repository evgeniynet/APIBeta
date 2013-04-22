using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/accounts")]
    [Route("/accounts/{user_id}")]
    public class Accounts : PagedApiRequest
    {
        public int? user_id { get; set; }
    }

    public class AccountsService : Service
    {
        [Secure()]
        public object Any(Accounts request)
        {
            ApiUser hdUser = request.ApiUser;
            if (!hdUser.IsTechAdmin)
                return Models.Accounts.GetUserAccounts(hdUser.OrganizationId, hdUser.DepartmentId, request.user_id ?? hdUser.UserId, request.page, request.limit);
            return Models.Accounts.GetAllAccounts(hdUser.OrganizationId, hdUser.DepartmentId, request.user_id ?? hdUser.UserId, request.page, request.limit);
        }
    }
}