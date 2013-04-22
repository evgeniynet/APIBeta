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
    [DataContract(Name = "ticket_log_records")]
    public class TicketLogRecords : ModelItemCollectionGeneric<TicketLogRecord>
    {
        public TicketLogRecords(DataTable RecordsTable) : base(RecordsTable) { }

        public static List<TicketLogRecord> TicketLog(Guid OrgId, int DeptId, int TktId, string TktPseudoId, int page, int limit)
        {
            TicketLogRecords _recs = new TicketLogRecords(bigWebApps.bigWebDesk.Data.Tickets.SelectTicketLog(OrgId, DeptId, TktId));
            foreach (TicketLogRecord _rec in _recs.List)
            {
                _rec.TktPseudoId = TktPseudoId;
            }
            return _recs.Skip(page * limit).Take(limit).ToList();
        }
    }
}