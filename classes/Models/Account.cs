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
    [DataContract(Name = "Account")]
    public class Account : ModelItemBase
    {
        public Account(DataRow row) : base(row) { }

        [DataMember(Name = "id")]
        public int Id
        {
            get { return Row.Table.Columns.Contains("AccountId") ? Convert.ToInt32(Row["AccountId"]) : Convert.ToInt32(Row["Id"]); }
            set
            {
                if (Row.Table.Columns.Contains("AccountId"))
                    Row["AccountId"] = value;
                else
                    Row["Id"] = value;
            }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row.Table.Columns.Contains("AccountName") ? Row["AccountName"].ToString() : Row["Name"].ToString(); }
            set
            {
                if (Row.Table.Columns.Contains("AccountName"))
                    Row["AccountName"] = value;
                else
                    Row["Name"] = value;
            }
        }
    }
}