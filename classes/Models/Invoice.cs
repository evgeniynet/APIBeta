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
    [DataContract(Name = "Invoice")]
    public class Invoice : ModelItemBase
    {
        public Invoice(DataRow row) : base(row) { }

        [DataMember(Name = "id")]
        public int Id
        {
            get { return Convert.ToInt32(Row["Id"]); }
            set { Row["Id"] = value; }
        }

        [DataMember(Name = "customer")]
        public string Customer
        {
            get { return Row["Customer"].ToString(); }
            set { Row["Customer"] = value; }
        }
        
        [DataMember(Name = "date")]
        public DateTime? Date
        {
            get
            {
                if (Row.Table.Columns.Contains("Date") && !Row.IsNull("Date")) return (DateTime)Row["Date"];
                else return null;
            }
            set
            {
                if (Row.Table.Columns.Contains("Date")) Row["Date"] = value;
            }
        }
        
        [DataMember(Name = "begin_date")]
        public DateTime? BeginDate
        {
            get
            {
                if (Row.Table.Columns.Contains("BeginDate") && !Row.IsNull("BeginDate")) return (DateTime)Row["BeginDate"];
                else return null;
            }
            set
            {
                if (Row.Table.Columns.Contains("BeginDate")) Row["BeginDate"] = value;
            }
        }
        
        [DataMember(Name = "end_date")]
        public DateTime? EndDate
        {
            get
            {
                if (Row.Table.Columns.Contains("EndDate") && !Row.IsNull("EndDate")) return (DateTime)Row["EndDate"];
                else return null;
            }
            set
            {
                if (Row.Table.Columns.Contains("EndDate")) Row["EndDate"] = value;
            }
        }

        [DataMember(Name = "total_hours")]
        public decimal TotalHours
        {
            get { return Convert.ToDecimal(Row["TotalHours"]); }
            set { Row["TotalHours"] = value; }
        }

        [DataMember(Name = "amount")]
        public decimal Amount
        {
            get { return Convert.ToDecimal(Row["Amount"]); }
            set { Row["Amount"] = value; }
        }

        [DataMember(Name = "travel_cost")]
        public decimal TravelCost
        {
            get { return Convert.ToDecimal(Row["TravelCost"]); }
            set { Row["TravelCost"] = value; }
        }

        [DataMember(Name = "total_cost")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(Row["TotalCost"]); }
            set { Row["TotalCost"] = value; }
        }

        [DataMember(Name = "is_fb_exported")]
        public bool FBExported
        {
            get { return Convert.ToBoolean(Row["FBExported"]); }
            set { Row["FBExported"] = value; }
        }

        [DataMember(Name = "is_qb_exported")]
        public bool QBExported
        {
            get { return Convert.ToBoolean(Row["QBExported"]); }
            set { Row["QBExported"] = value; }
        }
    }
}