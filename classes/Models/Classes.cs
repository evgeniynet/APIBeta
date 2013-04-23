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
    [DataContract(Name = "Classes")]
    public class Classes : ModelItemCollectionGeneric<Class>
    {
        public Classes(DataTable ClassesTable) : base(ClassesTable) { }

        public static List<Class> UserClasses(Guid OrgId, int DeptId, int UserId)
        {
            
            DataTable dtClasses = bigWebApps.bigWebDesk.Data.Classes.SelectByInactiveStatus(OrgId, DeptId, UserId, bigWebApps.bigWebDesk.Data.InactiveStatus.Active);
            Classes _classes = new Classes(dtClasses);
            return MakeTreeFromFlatList(_classes.List);
        }

        private static List<Class> MakeTreeFromFlatList(IEnumerable<Class> flatList)
        {
            var dic = flatList.ToDictionary(n => n.Id, n => n);
            var rootClasss = new List<Class>();
            foreach (var clas in flatList)
            {
                if (clas.ParentId > 0)
                {
                    Class parent = dic[clas.ParentId];
                    if ( parent.SubClasses == null)
                        parent.SubClasses = new List<Class>();
                    parent.SubClasses.Add(clas);
                }
                else
                {
                    rootClasss.Add(clas);
                }
            }
            return rootClasss;
        }
    }
}