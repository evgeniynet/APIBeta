using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Accounts")]
    public class Accounts : ModelItemCollectionGeneric<Account>
    {
        public Accounts(DataTable AccountsTable) : base(AccountsTable) { }

        public static List<Account> GetUserAccounts(Guid organizationId, int departmentId, int userId, int page, int limit)
        {
            Accounts _userAccounts = new Accounts(bigWebApps.bigWebDesk.Data.Accounts.SelectUserAccounts(departmentId,userId, organizationId));
            return _userAccounts.Skip(page * limit).Take(limit).ToList();
        }        
        
        public static List<Account> GetAllAccounts(Guid organizationId, int departmentId, int userId, int page, int limit)
        {
            Accounts _userAccounts = new Accounts(bigWebApps.bigWebDesk.Data.Accounts.SelectAll(organizationId, departmentId,userId));
            return _userAccounts.Skip(page * limit).Take(limit).ToList();
        }
    }
}