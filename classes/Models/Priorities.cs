using System;
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
    [DataContract(Name = "Priorities")]
    public class Priorities : ModelItemCollectionGeneric<Priority>
    {
        public Priorities(DataTable PrioritiesTable) : base(PrioritiesTable) { }

        public static List<Priority> All(Guid OrgId, int DeptId)
        {
            Priorities _priorities = new Priorities(bigWebApps.bigWebDesk.Data.Priorities.SelectAllFull(OrgId, DeptId));
            return _priorities.List;
        }
    }
}