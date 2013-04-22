using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Assets")]
    public class Assets : ModelItemCollectionGeneric<Asset>
    {
        public Assets(DataTable AssetsTable) : base(AssetsTable) { }

        public static List<Asset> TicketAssets(Guid OrgId, int DeptId, int TicketId)
        {
            var _assets = new Assets(bigWebApps.bigWebDesk.Data.Tickets.SelectAssets(OrgId, DeptId, TicketId));
            return _assets.List;
        }
    }
}