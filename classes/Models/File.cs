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
    [DataContract(Name = "File")]
    public class File : ModelItemBase
    {
        public File(DataRow row) : base(row) { }

        //[DataMember(Name = "id")]
        public int Id
        {
            get { return 1; }
            set { Row["Id"] = value; }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["Name"].ToString(); }
            set { Row["Name"] = value; }
        }
        
        [DataMember(Name = "url")]
        public string Url
        {
            get; set; 
        }

        //[DataMember(Name = "unique_id")]
        public string FileUniqueId
        {
            get { return Row["FileUniqueId"].ToString(); }
            set { Row["FileUniqueId"] = value; }
        }

        //[DataMember(Name = "department_id")]
        public Guid DepartmentId
        {
            get { return (Guid)Row["DepartmentId"]; }
            set { Row["DepartmentId"] = value; }
        }

        //[DataMember(Name = "organization_id")]
        public Guid OrganizationId
        {
            get { return (Guid)Row["OrganizationId"]; }
            set { Row["OrganizationId"] = value; }
        }
        
        [DataMember(Name = "date")]
        public DateTime? UpdatedTime
        {
            get
            {
                if (!Row.IsNull("UpdatedTime")) return (DateTime)Row["UpdatedTime"];
                else return null;
            }
            set
            {
                if (Row.Table.Columns.Contains("UpdatedTime")) Row["UpdatedTime"] = value;
            }
        }
        
        [DataMember(Name = "size")]
        public double SizeInBytes
        {
            get { return Convert.ToDouble(Row["SizeInBytes"]); }
            set { Row["SizeInBytes"] = value; }
        }

        [DataMember(Name = "is_deleted")]
        public bool Deleted
        {
            get { return Convert.ToBoolean(Row["Deleted"]); }
            set { Row["Deleted"] = value; }
        }
    }
}