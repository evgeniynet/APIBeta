
// Generated by MyGeneration Version # (1.2.0.2)

using System;
using System.Data;


using BWA.bigWebDesk.DAL;

namespace BWA.bigWebDesk.BLL
{
	public class BWDClass : BWA.bigWebDesk.DAL.tbl_class
	{
        public BWDClass(Guid organizationId)
        {
            this.ConnectionString = Micajah.Common.Bll.Providers.OrganizationProvider.GetConnectionString(organizationId);
        }

        public static int AddClass(Guid organizationId, int deptID,
                                    int userID,
                                    string className)
        {
            BWDClass cl = new BWDClass(organizationId);
            cl.AddNew();
            cl.Company_id = deptID;
            cl.LastResortTechId = userID;
            cl.Name = className;
            cl.ConfigDistributedRouting = 0;
            cl.TintClassType = 0;
            cl.BitRestrictToTechs = false;
            cl.BitAllowEmailParsing = false;
            cl.BtInactive = false;
            cl.Save();
            return cl.Id;
        }

        // for external use - web service
        public static DataSet GetClassList(Guid organizationId, int deptID)
        {
            BWDClass cl = new BWDClass(organizationId);
            cl.Where.Company_id.Value = deptID;
            cl.Where.Company_id.Operator = NCI.EasyObjects.WhereParameter.Operand.Equal;
            cl.Query.AddResultColumn(tbl_classSchema.Id);
            cl.Query.AddResultColumn(tbl_classSchema.Name);
            cl.Query.AddOrderBy(tbl_classSchema.Name);
            cl.Query.Load();
            if (cl.DefaultView.Table.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(cl.DefaultView.Table);
                return ds;
            }
            return null;
        }
	}
}
