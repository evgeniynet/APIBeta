using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk.Data;
using System.Net;
using ServiceStack.Common.Web;

namespace BWA.bigWebDesk.Api.Models
{
    [DataContract(Name = "fb_time_entry")]
    public class FBTimeEntries
    {
        public static string CreateTimeEntry(ApiUser hdUser, Instance_Config instanceConfig, int staffID, int projectID, int taskID, 
            decimal hours, string notes, DateTime date, int timeLogID, bool isProjectLog)
        {
            return FreshBooks.CreateTimeEntry(hdUser.OrganizationId, hdUser.DepartmentId, instanceConfig, instanceConfig.FBoAuthConsumerKey, instanceConfig.FBoAuthSecret,
                staffID, projectID, taskID, hours, notes, date, timeLogID, isProjectLog);
        }
    }
}