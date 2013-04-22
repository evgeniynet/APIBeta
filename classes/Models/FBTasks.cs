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
    [DataContract(Name = "fb_tasks")]
    public class FBTasks
    {
        public static List<FBTask> GetFBTasks(Instance_Config instanceConfig, int page, int limit, int projectID)
        {
            List<FBTask> arrTasks;
            string result = FreshBooks.GetTasksList(instanceConfig, out arrTasks, instanceConfig.FBoAuthConsumerKey, instanceConfig.FBoAuthSecret, limit, page, projectID);
            if (result == "ok")
            {
                return arrTasks;
            }
            else
            {
                throw new HttpError(HttpStatusCode.NotFound, result.Replace("\n", " ").Replace("\r", " "));
            }
        }
    }
}