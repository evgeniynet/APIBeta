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
    [DataContract(Name = "User_Accounts")]
    public class UserAccounts : ModelItemCollectionGeneric<UserAccount>
    {
        public UserAccounts(DataTable UserAccountsTable) : base(UserAccountsTable) { }

        public static List<UserAccount> Technicians(Guid OrgId, int DeptId, int page, int limit)
        {
            UserAccounts _users = new UserAccounts(bigWebApps.bigWebDesk.Data.Logins.SelectTechnicians(OrgId, DeptId));
            return _users.Skip(page * limit).Take(limit).ToList();
        }

        public static UserAccount GetUser(Guid OrgId, int DeptId, int UserId)
        {
            UserAccount _user = new UserAccount(bigWebApps.bigWebDesk.Data.Logins.SelectUserDetails(OrgId, DeptId, UserId));
            return _user;
        }

        public static List<UserAccount> FindUsers(Guid OrgId, int DeptId, int UserId, string Firstname, string Lastname, string Email, int page, int limit)
        {
            var _cfg = new Config(OrgId, DeptId);
            var _filter = new bigWebApps.bigWebDesk.Data.Logins.Filter("");
            if (!string.IsNullOrEmpty(Firstname))
            {
                _filter.FirstName = Firstname;
            }
            if (!string.IsNullOrEmpty(Lastname))
            {
                _filter.LastName = Lastname;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                _filter.EMail = Email;
            }
            _filter.UseGlobalFilters = true;
            _filter.SearchAccountsToo = false;
            _filter.ConfigAccountsEnabled = _cfg.AccountManager;
            _filter.ConfigLocationsEnabled = _cfg.LocationTracking;
            UserAccounts _users = new UserAccounts(bigWebApps.bigWebDesk.Data.Logins.SelectUsersByFilter(OrgId, DeptId, UserId, -1, _filter));
            return _users.Skip(page * limit).Take(limit).ToList();
        }
    }
}