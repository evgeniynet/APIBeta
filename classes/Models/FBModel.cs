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
    [DataContract(Name = "fb_data")]
    public class FBModel
    {
        public static void UpdateData(ApiUser User, int userID, int fbStaffID, int accountID, int fbClientId, int projectID,
            int fbProjectID, int taskTypeID, int fbTaskTypeID)
        {
            FreshBooks.UpdateData(User.OrganizationId, User.DepartmentId, userID, fbStaffID, accountID, fbClientId, projectID, fbProjectID, taskTypeID, fbTaskTypeID);
        }
    }
}