﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Levels
    /// </summary>
    [DataContract(Name = "Levels")]
    public class Levels : ModelItemCollectionGeneric<Level>
    {
        public Levels(DataTable LevelsTable) : base(LevelsTable) { }

        public static List<Level> UserLevels(Guid OrgId, int DeptId, int UserId)
        {
            Levels _levels = new Levels(bigWebApps.bigWebDesk.Data.Levels.SelectAll(OrgId, DeptId, UserId));
            return _levels.List;
        }
    }
}