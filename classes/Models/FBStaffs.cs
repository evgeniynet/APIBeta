﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk.Data;
using System.Net;
using ServiceStack.Common.Web;

namespace BWA.bigWebDesk.Api.Models
{
    [DataContract(Name = "fb_staff")]
    public class FBStaffs
    {
        public static List<FBStaff> GetFBStaff(Instance_Config instanceConfig, int page, int limit)
        {
            List<FBStaff> arrStaff;
            string result = FreshBooks.GetStaffList(instanceConfig, out arrStaff, instanceConfig.FBoAuthConsumerKey, instanceConfig.FBoAuthSecret, limit, page);
            if (result == "ok")
            {
                return arrStaff;
            }
            else
            {
                throw new HttpError(HttpStatusCode.NotFound, result.Replace("\n", " ").Replace("\r", " "));
            }
        }
    }
}