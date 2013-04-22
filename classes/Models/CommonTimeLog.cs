using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Class
    /// </summary>
    [DataContract(Name = "common_time_log")]
    public class CommonTimeLog : ModelItemBase
    {
        public CommonTimeLog(DataRow row) : base(row) { }

        [DataMember(Name = "time_id")]
        public int TimeLogID
        {
            get { return Convert.ToInt32(Row["id"]); }
            set { Row["id"] = value; }
        }

        [DataMember(Name = "project_name")]
        public string ProjectName
        {
            get { return Row["ProjectName"].ToString(); }
            set { Row["ProjectName"] = value; }
        }

        [DataMember(Name = "user_name")]
        public string UserName
        {
            get { return Row["UserName"].ToString(); }
            set { Row["UserName"] = value; }
        }

        [DataMember(Name = "user_email")]
        public string UserEmail
        {
            get { return Row["UserEmail"].ToString(); }
            set { Row["UserEmail"] = value; }
        }

        [DataMember(Name = "note")]
        public string Note
        {
            get { return Row["Note"].ToString(); }
            set { Row["Note"] = value; }
        }

        [DataMember(Name = "date")]
        public string Date
        {
            get
            {
                if (!Row.IsNull("LogDate")) return ((DateTime)Row["LogDate"]).ToString("dd MMM yyyy");
                else return null;
            }
            set
            {
                Row["LogDate"] = value;
            }
        }

        [DataMember(Name = "hours")]
        public decimal? Hours
        {
            get { return Convert.ToDecimal(Row["LogHours"]); }
            set { Row["LogHours"] = value; }
        }

        [DataMember(Name = "fb_id")]
        public int FBId
        {
            get { return Convert.ToInt32(Row["FBID"]); }
            set { Row["FBID"] = value; }
        }

        [DataMember(Name = "is_project_log")]
        public bool IsProjectLog
        {
            get { return Convert.ToBoolean(Row["IsProjectLog"]); }
            set { Row["IsProjectLog"] = value; }
        }

        [DataMember(Name = "ticket_id")]
        public int TicketID
        {
            get { return Convert.ToInt32(Row["TicketID"]); }
            set { Row["TicketID"] = value; }
        }

        [DataMember(Name = "project_id")]
        public int ProjectID
        {
            get { return Convert.ToInt32(Row["ProjectID"]); }
            set { Row["ProjectID"] = value; }
        }

        [DataMember(Name = "account_id")]
        public int AccountID
        {
            get { return Convert.ToInt32(Row["AccountID"]); }
            set { Row["AccountID"] = value; }
        }

        [DataMember(Name = "ticket_number")]
        public int TicketNumber
        {
            get { return Convert.ToInt32(Row["TicketNumber"]); }
            set { Row["TicketNumber"] = value; }
        }

        [DataMember(Name = "account_name")]
        public string AccountName
        {
            get { return Row["AccountName"].ToString(); }
            set { Row["AccountName"] = value; }
        }
    }
}