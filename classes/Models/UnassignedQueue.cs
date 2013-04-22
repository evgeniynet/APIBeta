using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecord
    /// </summary>
    [DataContract(Name = "Unassigned_Queue")]
    public class UnassignedQueue : ModelItemBase
    {
        public UnassignedQueue(DataRow row) : base(row) { }

        //[DataMember(Name = "email")]
        public string Email
        {
            get { return Row["QueEmailAddress"].ToString(); }
            set { Row["QueEmailAddress"] = value; }
        }

        [DataMember(Name = "fullname")]
        public string FullName
        {
            get { return Row["FullName"].ToString().Replace(" Queue", ""); }
            set { Row["FullName"] = value; }
        }

        [DataMember(Name = "tickets_count")]
        public int TicketsCount
        {
            get { return Row.Table.Columns.Contains("TicketsCount") ? (int)Row["TicketsCount"] : 0; }
            set { if (Row.Table.Columns.Contains("TicketsCount")) Row["TicketsCount"] = value; }
        }
    }
}