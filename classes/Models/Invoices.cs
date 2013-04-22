using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Invoices")]
    public class Invoices : ModelItemCollectionGeneric<Invoice>
    {
        public Invoices(DataTable InvoicesTable) : base(InvoicesTable) { }

        public static List<Invoice> GetInvoices(Guid organizationId, int departmentId, int userId)
        {
            DateTime beginDate, endDate;
            endDate = DateTime.UtcNow;
            beginDate = endDate.AddDays(-30);
            Invoices _invoices = new Invoices(bigWebApps.bigWebDesk.Data.Invoice.SelectInvoices(organizationId, departmentId, beginDate, endDate, false));
            return _invoices.List;
        }
    }
}