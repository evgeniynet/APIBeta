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
    [DataContract(Name = "fb_projects")]
    public class FBProjects
    {
        public static List<FBProject> GetFBProjects(Instance_Config instanceConfig, int page, int limit, int clientID, int staffID)
        {
            List<FBProject> arrProjects;
            string result = FreshBooks.GetProjectsList(instanceConfig, out arrProjects, instanceConfig.FBoAuthConsumerKey, instanceConfig.FBoAuthSecret, limit, page, clientID, staffID);
            if (result == "ok")
            {
                return arrProjects;
            }
            else
            {
                throw new HttpError(HttpStatusCode.NotFound, result.Replace("\n", " ").Replace("\r", " "));
            }
        }
    }
}