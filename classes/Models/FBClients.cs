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
    [DataContract(Name = "fb_clients")]
    public class FBClients
    {
        public static List<FBClient> GetFBClients(Instance_Config instanceConfig, int page, int limit)
        {
            List<FBClient> arrClients;
            string result = FreshBooks.GetClientList(instanceConfig, out arrClients, instanceConfig.FBoAuthConsumerKey, instanceConfig.FBoAuthSecret, limit, page);
            if (result == "ok")
            {
                return arrClients;
            }
            else
            {
                throw new HttpError(HttpStatusCode.NotFound, result.Replace("\n", " ").Replace("\r", " "));
            }
        }
    }
}