using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Priority
    /// </summary>
    [DataContract(Name = "Priority")]
    public class Priority : ModelItemBase
    {
        public Priority(DataRow row) : base(row) { }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["Name"].ToString(); }
            set { Row["Name"] = value; }
        }

        [DataMember(Name = "priority_level")]
        public int PriorityLevel
        {
            get { return Convert.ToInt32(Row["tintPriority"]); }
            set { Row["tintPriority"] = value; }
        }

        [DataMember(Name = "description")]
        public string Description
        {
            get { return Row["Description"].ToString(); }
            set { Row["Description"] = value; }
        }
    }
}