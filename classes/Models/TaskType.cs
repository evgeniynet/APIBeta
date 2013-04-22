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
    [DataContract(Name = "TaskType")]
    public class TaskType : ModelItemBase
    {
        public TaskType(DataRow row) : base(row)
        {
            IdFieldName = "ttID";
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["TaskTypeName"].ToString(); }
            set { Row["TaskTypeName"] = value; }
        }
    }
}