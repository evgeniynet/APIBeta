using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using ServiceStack.Common.Web;
using System.Net;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/invoices")]
    [Route("/invoices/{id}")]
    public class Invoices : ApiRequest
    {
        public int? id { get; set; }
    }

    public class InvoicesService : Service
    {
        [Secure()]
        public object Get(Invoices request)
        {
            ApiUser hdUser = request.ApiUser;
            if (request.id == null)
                return Models.Invoices.GetInvoices(hdUser.OrganizationId, hdUser.DepartmentId, hdUser.UserId);
            return new HttpResult("", HttpStatusCode.NotFound);
        }
    }
}