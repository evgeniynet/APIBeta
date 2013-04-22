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
    [DataContract(Name = "Worklist_Ticket")]
    public class WorklistTicket : ModelItemBase
    {
        public WorklistTicket(DataRow row) : base(row) { }

        [DataMember(Name = "key")]
        public string PseudoID
        {
            get { return Row["PseudoId"].ToString(); }
            set { Row["PseudoId"] = value; }
        }

        [DataMember(Name = "created_time")]
        public DateTime? CreateTime
        {
            get { if (!Row.IsNull("CreateTime")) return (DateTime)Row["CreateTime"]; else return null; }
            set { if (value.HasValue) Row["CreateTime"] = value.Value; else Row["CreateTime"] = DBNull.Value; }
        }

        [DataMember(Name = "number")]
        public int TicketNumber
        {
            get { return (int)Row["TicketNumber"]; }
            set { Row["TicketNumber"] = value.ToString(); }
        }

        [DataMember(Name = "is_new_user_post")]
        public bool NewUserPost
        {
            get { return (bool)Row["NewUserPost"]; }
            set { Row["NewUserPost"] = value; }
        }

        [DataMember(Name = "is_new_tech_post")]
        public bool NewTechPost
        {
            get { return (bool)Row["NewTechPost"]; }
            set { Row["NewTechPost"] = value; }
        }  
       
        [DataMember(Name = "prefix")]
        public string TicketNumberPrefix
        {
            get { return Row["TicketNumberPrefix"].ToString(); }
            set { Row["TicketNumberPrefix"] = value; }
        }

        [DataMember(Name = "subject")]
        public string Subject
        {
            get { return Row["Subject"].ToString(); }
            set { Row["Subject"] = value; }
        }

        [DataMember(Name = "user_firstname")]
        public string UserFirstName
        {
            get { return Row["user_firstname"].ToString(); }
            set { Row["user_firstname"] = value; }
        }

        [DataMember(Name = "user_lastname")]
        public string UserLastName
        {
            get { return Row["user_lastname"].ToString(); }
            set { Row["user_lastname"] = value; }
        }

        [DataMember(Name = "user_email")]
        public string UserEmail
        {
            get { return Row["user_email"].ToString(); }
            set { Row["user_email"] = value; }
        }

        [DataMember(Name = "technician_firstname")]
        public string TechnicianFirstName
        {
            get { return Row["technician_firstname"].ToString(); }
            set { Row["technician_firstname"] = value; }
        }

        [DataMember(Name = "technician_lastname")]
        public string TechnicianLastName
        {
            get { return Row["technician_lastname"].ToString(); }
            set { Row["technician_lastname"] = value; }
        }

        [DataMember(Name = "technician_email")]
        public string TechnicianEmail
        {
            get { return Row["technician_email"].ToString(); }
            set { Row["technician_email"] = value; }
        }

        [DataMember(Name = "account_name")]
        public string AccountName
        {
            get { return Row["vchAcctName"].ToString(); }
            set { Row["vchAcctName"] = value; }
        }

        [DataMember(Name = "priority_name")]
        public string PriorityName
        {
            get { return Row["PriName"].ToString(); }
            set { Row["PriName"] = value; }
        }

        [DataMember(Name = "level_name")]
        public string LevelName
        {
            get { return Row["LevelName"].ToString(); }
            set { Row["LevelName"] = value; }
        }

        [DataMember(Name = "status")]
        public bigWebApps.bigWebDesk.Data.Ticket.Status TicketStatus
        {
            get
            {
                switch (Row["Status"].ToString())
                {
                    case "Open":
                        return bigWebApps.bigWebDesk.Data.Ticket.Status.Open;
                    case "Closed":
                        return bigWebApps.bigWebDesk.Data.Ticket.Status.Closed;
                    case "On Hold":
                        return bigWebApps.bigWebDesk.Data.Ticket.Status.OnHold;
                    case "Parts On Order":
                        return bigWebApps.bigWebDesk.Data.Ticket.Status.PartsOnOrder;
                }
                return bigWebApps.bigWebDesk.Data.Ticket.Status.Open;
            }
            set
            {
                switch (value)
                {
                    case bigWebApps.bigWebDesk.Data.Ticket.Status.Open:
                        Row["Status"] = "Open";
                        break;
                    case bigWebApps.bigWebDesk.Data.Ticket.Status.Closed:
                        Row["Status"] = "Closed";
                        break;
                    case bigWebApps.bigWebDesk.Data.Ticket.Status.OnHold:
                        Row["Status"] = "On Hold";
                        break;
                    case bigWebApps.bigWebDesk.Data.Ticket.Status.PartsOnOrder:
                        Row["Status"] = "Parts On Order";
                        break;
                    default:
                        Row["Status"] = "Open";
                        break;
                }
            }
        }
    }
}