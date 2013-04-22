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
    [DataContract(Name = "Tickets_Count")]
    public class TicketCount : ModelItemBase
    {
        public TicketCount(DataRow row) : base(row) { }

        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "new")]
        public int New
        {
            get { return Row.IsNull("NewMessagesCount") ? 0 : (int)Row["NewMessagesCount"]; }
            set { Row["NewMessagesCount"] = value; }
        }

        [DataMember(Name = "open")]
        public int Open
        {
            get { return Row.IsNull("OpenTickets") ? 0 : (int)Row["OpenTickets"]; }
            set { Row["OpenTickets"] = value; }
        }

        [DataMember(Name = "total")]
        public int Total
        {
            get { return Row.IsNull("userTickets") ? 0 : (int)Row["userTickets"]; }
            set { Row["userTickets"] = value; }
        }

        [DataMember(Name = "onhold")]
        public int OnHold
        {
            get { return Row.IsNull("OnHoldTickets") ? 0 : (int)Row["OnHoldTickets"]; }
            set { Row["OnHoldTickets"] = value; }
        }

        [DataMember(Name = "reminder")]
        public int Reminder
        {
            get { return Row.IsNull("reminderTicket") ? 0 : (int)Row["reminderTicket"]; }
            set { Row["reminderTicket"] = value; }
        }

        [DataMember(Name = "parts_on_order")]
        public int PartsOnOrder
        {
            get { return Row.IsNull("PartsOnOrderTickets") ? 0 : (int)Row["PartsOnOrderTickets"]; }
            set { Row["PartsOnOrderTickets"] = value; }
        }

        [DataMember(Name = "unconfirmed")]
        public int Unconfirmed
        {
            get { return Row.IsNull("UnconfirmedUserTickets") ? 0 : (int)Row["UnconfirmedUserTickets"]; }
            set { Row["UnconfirmedUserTickets"] = value; }
        } 
    }
}