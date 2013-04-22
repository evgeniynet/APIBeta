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
    [DataContract(Name = "Instance_Config")]
    public class Instance_Config : bigWebApps.bigWebDesk.Config
    {
        private int m_TimeZoneOffset;
        private int m_BusinessDayLength;
        private string m_TimeZoneId;

        public Instance_Config(ApiUser usr) : base(usr.OrganizationId, usr.DepartmentId)
        {
            User=new UserConfig(usr);
            Assets = new AssetsConfig(base.Assets);
            m_TimeZoneOffset = usr.TimeZoneOffset;
            m_TimeZoneId = usr.TimeZoneId;
            m_BusinessDayLength = GetBusinessDayLength(base.BusHourStart, base.BusMinStart, base.BusHourStop, base.BusMinStop);
        }

        private int GetBusinessDayLength(int BusHourStart, int BusMinStart, int BusHourStop, int BusMinStop)
        {
            int _result = 1440;

            DateTime current_time = DateTime.UtcNow.Date;
            int hours = current_time.Hour;
            int mins = current_time.Minute;
            int secs = current_time.Second;
            int msecs = current_time.Millisecond;

            current_time = current_time.AddHours(-hours);
            current_time = current_time.AddMinutes(-mins);
            current_time = current_time.AddSeconds(-secs);
            current_time = current_time.AddMilliseconds(-msecs);

            DateTime start_time, end_time;
            start_time = current_time;
            end_time = current_time;

            hours = BusHourStart;
            mins = BusMinStart;

            start_time = start_time.AddHours(hours);
            start_time = start_time.AddMinutes(mins);

            hours = BusHourStop;
            mins = BusMinStop;

            end_time = end_time.AddHours(hours);
            end_time = end_time.AddMinutes(mins);

            if (end_time > start_time)
            {
                TimeSpan _ts = end_time - start_time;
                _result = (int)_ts.TotalMinutes;
            };

            return _result;
        }

        [DataMember(Name = "is_onhold_status")]
        new public bool OnHoldStatus
        {
            get { return base.OnHoldStatus; }
            set { base.OnHoldStatus = value; }
        }

        [DataMember(Name = "is_time_tracking")]
        new public bool TimeTracking
        {
            get { return base.TimeTracking; }
            set { base.TimeTracking = value; }
        }

        [DataMember(Name = "is_parts_tracking")]
        new public bool PartsTracking
        {
            get { return base.PartsTracking; }
            set { base.PartsTracking = value; }
        }

        [DataMember(Name = "is_project_tracking")]
        new public bool ProjectTracking
        {
            get { return base.ProjectTracking; }
            set { base.ProjectTracking = value; }
        }

        [DataMember(Name = "is_unassigned_queue")]
        new public bool UnassignedQue
        {
            get { return base.UnassignedQue; }
            set { base.UnassignedQue = value; }
        }

        [DataMember(Name = "is_location_tracking")]
        new public bool LocationTracking
        {
            get { return base.LocationTracking; }
            set { base.LocationTracking = value; }
        }

        [DataMember(Name = "is_class_tracking")]
        new public bool ClassTracking
        {
            get { return base.ClassTracking; }
            set { base.ClassTracking = value; }
        }

        [DataMember(Name = "is_priorities_general")]
        new public bool PrioritiesGeneral
        {
            get { return base.PrioritiesGeneral; }
            set { base.PrioritiesGeneral = value; }
        }

        [DataMember(Name = "is_confirmation_tracking")]
        new public bool ConfirmationTracking
        {
            get { return base.ConfirmationTracking; }
            set { base.ConfirmationTracking = value; }
        }

        [DataMember(Name = "is_ticket_levels")]
        new public bool TktLevels
        {
            get { return base.TktLevels; }
            set { base.TktLevels = value; }
        }

        [DataMember(Name = "is_account_manager")]
        new public bool AccountManager
        {
            get { return base.AccountManager; }
            set { base.AccountManager = value; }
        }

        [DataMember(Name = "is_require_ticket_initial_post")]
        new public bool RequireTktInitialPost
        {
            get { return base.RequireTktInitialPost; }
            set { base.RequireTktInitialPost = value; }
        }

        [DataMember(Name = "is_ticket_require_closure_note")]
        new public bool TktRequireClosureNote
        {
            get { return base.TktRequireClosureNote; }
            set { base.TktRequireClosureNote = value; }
        }

        [DataMember(Name = "is_asset_tracking")]
        new public bool AssetTracking
        {
            get { return base.AssetTracking; }
            set { base.AssetTracking = value; }
        }

        [DataMember(Name = "assets")]
        new public AssetsConfig Assets
        { get; protected set; }

        [DataMember(Name = "timezone_offset")]
        public int TimeZoneOffset
        {
            get { return m_TimeZoneOffset; }
        }

        [DataMember(Name = "timezone_name")]
        public string TimeZoneId
        {
            get { return m_TimeZoneId; }
        }

        [DataMember(Name = "businessday_length")]
        public int BusinessDayLength
        {
            get { return m_BusinessDayLength; }
        }

        public string FBoAuthConsumerKey
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["FBoAuthConsumerKey"]))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["FBoAuthConsumerKey"].ToString();
                }
                return "";
            }
        }

        public string FBoAuthSecret
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["FBoAuthSecret"]))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["FBoAuthSecret"].ToString();
                }
                return "";
            }
        }

        [DataMember(Name = "user")]
        public UserConfig User { get; protected set; }
    }
}