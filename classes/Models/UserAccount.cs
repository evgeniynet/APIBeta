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
    [DataContract(Name = "User_Account")]
    public class UserAccount : ModelItemBase
    {
        public UserAccount(DataRow row) : base(row) { }

        [DataMember(Name = "email")]
        public string Email
        {
            get { return Row["Email"].ToString(); }
            set { Row["Email"] = value; }
        }

        [DataMember(Name = "firstname")]
        public string FirstName
        {
            get { return Row["FirstName"].ToString(); }
            set { Row["FirstName"] = value; }
        }

        [DataMember(Name = "lastname")]
        public string LastName
        {
            get { return Row["LastName"].ToString(); }
            set { Row["LastName"] = value; }
        }

        [DataMember(Name = "type")]
        public string Type
        {
            get { return Row.Table.Columns.Contains("UserType_Id") ? GetUserType((int)Row["UserType_Id"]) : "tech"; }
            set { Row["UserType_Id"] = value.ToString(); }
        }

        private string GetUserType(int userType)
        {
            switch (userType)
            {
                case 1:
                case 5: return "user";
                case 2: return "tech";
                case 3: return "admin";
            }
            return "queue";
        }
    }
}