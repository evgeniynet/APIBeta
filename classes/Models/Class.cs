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
    [DataContract(Name = "Class")]
    public class Class : ModelItemBase
    {
        public Class(DataRow row) : base(row) { }

        private List<Class> _subClasses = null;

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["Name"].ToString(); }
            set { Row["Name"] = value; }
        }
        
        [DataMember(Name = "id")]
        public int Id
        {
            get { return Convert.ToInt32(Row["Id"]); }
            set { Row["Id"] = value; }
        }
        
        //[DataMember(Name = "parent_id")]
        public int ParentId
        {
            get { return Row.IsNull("ParentId") ? 0 : (int)Row["ParentId"]; }
            set { Row["ParentId"] = value; }
        }

        //[DataMember(Name = "hierarchy_level")]
        public int HierarchyLevel
        {
            get { return Convert.ToInt32(Row["Level"]); }
            set { Row["Level"] = value; }
        }

        [DataMember(Name = "sub")]
        public List<Class> SubClasses
        {
            get { return _subClasses; }
            set
            {
                _subClasses = value;
            }
        }

        //[DataMember(Name = "is_lastchild")]
        public bool IsLastChild
        {
            get { return (bool)Row["IsLastChild"]; }
            set { Row["IsLastChild"] = value; }
        }
    }
}