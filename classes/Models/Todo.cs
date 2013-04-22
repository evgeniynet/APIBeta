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
    [DataContract(Name = "Todo")]
    public class Todo : ModelItemBase
    {
        public Todo(DataRow row) : base(row) { }

        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "list_id")]
        public Guid ToDoListId
        {
            get { return Guid.Parse(Row["ToDoListId"].ToString()); }
            set { Row["ToDoListId"] = value; }
        }

        [DataMember(Name = "id")]
        public Guid? ToDoItemId
        {
            get
            {
                if (!Row.IsNull("ToDoItemId")) return (Guid)Row["ToDoItemId"];
                else return null;
            }
            set { Row["ToDoItemId"] = value; }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return Row["Name"].ToString(); }
            set { Row["Name"] = value; }
        }

        [DataMember(Name = "text")]
        public string Text
        {
            get { return Row["Text"].ToString(); }
            set { Row["Text"] = value; }
        }

        [DataMember(Name = "order_list")]
        public int OrderList
        {
            get { return Convert.ToInt32(Row["OrderList"]); }
            set { Row["OrderList"] = value; }
        }

        [DataMember(Name = "order_item")]
        public int OrderItem
        {
            get { return Convert.ToInt32(Row["OrderItem"]); }
            set { Row["OrderItem"] = value; }
        }

        [DataMember(Name = "assigned_id")]
        public int? AssignedId
        {
            get
            {
                if (!Row.IsNull("AssignedId")) return Convert.ToInt32(Row["AssignedId"]);
                else return null; 
            }
            set { Row["AssignedId"] = value; }
        }

        [DataMember(Name = "assigned_name")]
        public string AssignedName
        {
            get { return Row["AssignedName"].ToString(); }
            set { Row["AssignedName"] = value; }
        }

        [DataMember(Name = "type")]
        public int ItemType
        {
            get { return Convert.ToInt32(Row["ItemType"]); }
            set { Row["ItemType"] = value; }
        }
        
        [DataMember(Name = "due_date")]
        public DateTime? DueDate
        {
            get
            {
                if (!Row.IsNull("Due")) return (DateTime)Row["Due"];
                else return null;
            }
            set
            {
                Row["Due"] = value;
            }
        }

        [DataMember(Name = "is_completed")]
        public bool Completed
        {
            get { return Convert.ToBoolean(Row["Completed"]); }
            set { Row["Completed"] = value; }
        }
    }
}