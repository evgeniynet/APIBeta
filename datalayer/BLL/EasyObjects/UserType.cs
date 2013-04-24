
// Generated by MyGeneration Version # (1.2.0.2)

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NCI.EasyObjects;

namespace BWA.bigWebDesk.BLL
{
	public class UserType : BWA.bigWebDesk.DAL.tbl_UserType
	{
        public enum BWDUser
        {
            STANDARD_USER = 1,
            TECHNICIAN = 2,
            ADMINISTRATOR = 3,
            QUEUE = 4,
            SUPER_USER = 5
        }

        public UserType(Guid organizationId)
        {
            this.ConnectionString = Micajah.Common.Bll.Providers.OrganizationProvider.GetConnectionString(organizationId);
        }

        public virtual IDataReader SelectUserTypes()
        {
            //  Create the Database object, using the default database service. The
            //  default database service is determined through configuration.
            Database db = GetDatabase();

            string sqlCommand = this.SchemaStoredProcedureWithSeparator + "sp_SelectUserTypes";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            // Add procedure parameters

            return base.LoadFromSqlReader(dbCommand);
        }
	}
}