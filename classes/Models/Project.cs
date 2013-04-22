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
    [DataContract(Name = "Project")]
    public class Project : ModelItemBase
    {
        public Project(DataRow row) : base(row)  {  }

        [DataMember(Name = "id")]
        public int Id
        {
            get { return Convert.ToInt32(Row["ProjectID"]); }
            set { Row["ProjectID"] = value; }
        }

        [DataMember(Name = "account_id")]
        public int AccountId
        {
            get { return Convert.ToInt32(Row["AccountID"]); }
            set { Row["AccountID"] = value; }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["Name"].ToString(); }
            set { Row["Name"] = value; }
        }
        
        [DataMember(Name = "account_name")]
        public string AccountName
        {
            get { return Row["AccountName"].ToString(); }
            set { Row["AccountName"] = value; }
        }

        [DataMember(Name = "open_tickets")]
        public int OpenTickets
        {
            get { return Convert.ToInt32(Row["OpenTickets"]); }
            set { Row["OpenTickets"] = value; }
        }

        [DataMember(Name = "closed_tickets")]
        public int ClosedTickets
        {
            get { return Convert.ToInt32(Row["ClosedTickets"]); }
            set { Row["ClosedTickets"] = value; }
        }

        [DataMember(Name = "priority")]
        public int? Priority
        {
            get
            {
                if (!Row.IsNull("tintPriority"))
                    return Convert.ToInt32(Row["tintPriority"]);
                return null;
            }
            set { Row["tintPriority"] = value; }
        }

        [DataMember(Name = "priority_name")]
        public string PriorityName
        {
            get { return Row["PriorityName"].ToString(); }
            set { Row["PriorityName"] = value; }
        }
        
        
        [DataMember(Name = "logged_hours")]
        public decimal? TotalHours
        {
            get { if (Row.IsNull("TotalHours")) return null; return Convert.ToDecimal(Row["TotalHours"]); }
            set { Row["TotalHours"] = value; }
        }

        [DataMember(Name = "remaining_hours")]
        public decimal? RemainingHours
        {
            get { if (Row.IsNull("RemainingHours")) return null; return Convert.ToDecimal(Row["RemainingHours"]); }
            set { Row["RemainingHours"] = value; }
        }

        [DataMember(Name = "complete")]
        public int Complete
        {
            get
            {
                if (!Row.IsNull("TotalHours") && !Row.IsNull("RemainingHours"))
                {
                    decimal remainingHours = Convert.ToDecimal(Row["RemainingHours"]);
                    decimal loggedHours = Convert.ToDecimal(Row["TotalHours"]);
                    decimal totalHours = remainingHours + loggedHours;
                    if (totalHours > 0)
                    {
                        return ((int)Math.Round(loggedHours / totalHours * 100));
                    }
                }
                return 0;
            }
        }

        [DataMember(Name = "client_manager")]
        public string ClientManager
        {
            get { return Row["PMFullName"].ToString(); }
            set { Row["PMFullName"] = value; }
        }
    }
}