using System;
using System.Net;
using System.Data;
using Micajah.Common.Bll;
using Micajah.Common.Bll.Providers;
using ServiceStack.Common.Web;
using bigWebApps.bigWebDesk.Data;
using ServiceStack.ServiceHost;

namespace BWA.bigWebDesk.Api
{
    /// <summary>
    /// Summary description for ApiUser
    /// </summary>
    public class ApiUser
    {
        protected Guid m_LoginId;
        protected Guid m_OrganizationId;
        protected Guid m_InstanceId;
        protected int m_DepartmentId;
        protected int m_UserId;
        protected int m_AccountId;
        protected string m_LoginEmail;
        protected bool m_IsTechAdmin;
        protected bool m_IsUseWorkDaysTimer;
        protected string m_FirstName;
        protected string m_LastName;
        protected string[] m_Roles;
        protected int m_TimeZoneOffset;
        protected string m_TimeZoneId;
        const int DefaultTimeZoneOffset = -5;

        public ApiUser(string apiKey)
        {
            m_LoginId = Guid.Empty;
            m_OrganizationId = Guid.Empty;
            m_InstanceId = Guid.Empty;
            m_DepartmentId = 0;
            m_UserId = 0;
            m_LoginEmail = string.Empty;
            m_IsTechAdmin = false;
            m_IsUseWorkDaysTimer = false;
            m_Roles=new string[0];
            Micajah.Common.Bll.Providers.LoginProvider lp = new Micajah.Common.Bll.Providers.LoginProvider();
            DataRowView userRow = lp.GetLoginByToken(apiKey);
            if (userRow == null) throw new HttpError(HttpStatusCode.NotFound, "User with token \"" + apiKey + "\" was not found.");
            m_LoginId = (Guid)userRow["LoginId"];
            OrganizationCollection _orgs=Micajah.Common.Application.WebApplication.LoginProvider.GetOrganizationsByLoginId(m_LoginId);
            if (_orgs.Count==0) throw new HttpError(HttpStatusCode.NotFound, "No assigned organizations found for this user.");
            m_LoginEmail = userRow["LoginName"].ToString();
            //m_LoginId = userRow.UserId;
            m_FirstName = userRow["FirstName"].ToString();
            m_LastName = userRow["LastName"].ToString();
        }

        private void CompleteInitObject()
        {
            var CurrOrganization = Micajah.Common.Bll.Providers.OrganizationProvider.GetOrganization(m_OrganizationId);
            byte _graceDays = (byte)CurrOrganization.GraceDays;
            DateTime _expire = DateTime.UtcNow.AddMonths(1);
            if (CurrOrganization.ExpirationTime != null)
            {
                _expire = (DateTime)CurrOrganization.ExpirationTime;
            }
            DateTime _now = DateTime.UtcNow;
            _expire = _expire.AddDays(_graceDays);
            if (_expire < _now)
            {
                throw new HttpError(HttpStatusCode.Forbidden, "Your organization's account has expired.");
            }

            DataRow _row = bigWebApps.bigWebDesk.Data.Companies.SelectOne(m_OrganizationId, m_InstanceId);
            if (_row == null) throw new HttpError(HttpStatusCode.NotFound, "Can't find department for OrganizationId/InstanceId=" + OrganizationId.ToString() + "/" + InstanceId.ToString());
            m_DepartmentId = (int)_row["company_id"];
            int _userStatus = Logins.SelectLoginExists(m_OrganizationId, m_DepartmentId, m_LoginEmail, out m_UserId, out m_AccountId);
            if (_userStatus == 1) throw new HttpError(HttpStatusCode.NotFound, "Can't find User \"" + m_LoginEmail + "\" in the bigWebApps database.");
            else if (_userStatus == 2) throw new HttpError(HttpStatusCode.Forbidden, "User \""+m_LoginEmail+"\" is not associated with OrganizationId/InstanceId=" + OrganizationId.ToString() + "/" + InstanceId.ToString() + ".");
            _row = Logins.SelectUserDetails(m_OrganizationId, m_DepartmentId, m_UserId);
            if (_row == null) throw new HttpError(HttpStatusCode.Forbidden, "The user account is not associated with this Department.");
            Micajah.Common.Dal.OrganizationDataSet.UserRow _uRow = UserProvider.GetUserRow(m_LoginEmail);
            string TimeZoneId = null;
            if (_uRow != null && !_uRow.IsTimeFormatNull()) 
                TimeZoneId = _uRow.TimeZoneId;
            if (string.IsNullOrEmpty(TimeZoneId)  && !_row.IsNull("InstanceTimeZoneId"))
                TimeZoneId = _row["InstanceTimeZoneId"].ToString();
            try
            {
                m_TimeZoneOffset = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId).BaseUtcOffset.Hours;
                m_TimeZoneId = TimeZoneId;
            }
            catch
            {
                m_TimeZoneOffset = DefaultTimeZoneOffset;
            }
            if ((bool)_row["btUserInactive"]) throw new HttpError(HttpStatusCode.Forbidden, "The user account is Inactive in this Department.");
            if (!_row.IsNull("tintTicketTimer")) m_IsUseWorkDaysTimer = (byte)_row["tintTicketTimer"] > 0;
            else m_IsUseWorkDaysTimer = (byte)_row["tintDTicketTimer"] > 0;
            if ((int)_row["UserType_Id"] == 2 || (int)_row["UserType_Id"] == 3) m_IsTechAdmin = true;
        }

        public bool Validate(string userName, string password)
        {
            var _lp = new LoginProvider();
            return _lp.ValidateLogin(userName, password);
        }

        public static bool ValidateStatic(string userName, string password)
        {
            var _lp = new LoginProvider();
            return _lp.ValidateLogin(userName, password);
        }

        public static ApiUser getUser(IHttpRequest request)
        {
            var basicAuth = request.GetBasicAuthUserAndPassword();
            string key = basicAuth.Value.Key;
            string api_token = basicAuth.Value.Value;
            if (key.Length != 13 || !key[6].Equals('-'))
                throw new HttpError(HttpStatusCode.Forbidden, "Org/Instance is not correct.");
            string[] split = key.Split('-');
            string org_key = split[0];
            string instance_key = split[1];
            if (api_token.Length != 32)
                throw new HttpError(HttpStatusCode.Forbidden, "Token is not correct.");
            ApiUser apiUser = new ApiUser(api_token);
            if (!apiUser.ValidateAccess(org_key, instance_key)) 
                throw new HttpError(HttpStatusCode.Forbidden, "User is Inactive or does not have access to this Organization/Instance");
            return apiUser;
        }

        public static bool IsExists(string userName)
        {
            Micajah.Common.Dal.OrganizationDataSet.UserRow _uRow = UserProvider.GetUserRow(userName);
            if (_uRow == null) return false;
            return true;
        }

        public bool ValidateAccess(string orgKey, string instKey)
        {
            if (string.IsNullOrEmpty(orgKey)) throw new HttpError(HttpStatusCode.InternalServerError, new ArgumentException("OrgKey parameter can't be null or empty"));

            if (!string.IsNullOrEmpty(instKey))
            {
                Micajah.Common.Bll.Organization _org = Micajah.Common.Bll.Providers.OrganizationProvider.GetOrganizationByPseudoId(orgKey);
                if (_org == null) throw new HttpError(HttpStatusCode.NotFound, "Can't find organization with OrganizationKey=" + orgKey);

                Micajah.Common.Bll.Instance _inst = Micajah.Common.Bll.Providers.InstanceProvider.GetInstanceByPseudoId(instKey, _org.OrganizationId);
                if (_inst == null) throw new HttpError(HttpStatusCode.NotFound, "Can't find instance with InstanceKey=" + instKey);

                if (UserProvider.UserIsActiveInInstance(LoginId, _inst.InstanceId, _org.OrganizationId))
                {
                    m_OrganizationId = _org.OrganizationId;
                    m_InstanceId = _inst.InstanceId;
                    CompleteInitObject();
                    return true;
                }
            }
            else
            {
                var _lp = new LoginProvider();
                Micajah.Common.Bll.OrganizationCollection _orgsMc = _lp.GetOrganizationsByLoginId(LoginId);
                foreach (Organization _org in _orgsMc)
                {
                    if (_org.PseudoId == orgKey)
                    {
                        m_OrganizationId = _org.OrganizationId;
                        return true;
                    }
                }
            }
            return false;
        }

        public Guid LoginId
        {
            get { return m_LoginId; }
        }

        public string LoginEmail
        {
            get { return m_LoginEmail; }
        }

        public Guid OrganizationId
        {
            get { return m_OrganizationId; }
        }

        public Guid InstanceId
        {
            get { return m_InstanceId; }
        }

        public int DepartmentId
        {
            get { return m_DepartmentId; }
        }

        public int UserId
        {
            get { return m_UserId; }
        }

        public bool IsTechAdmin
        {
            get { return m_IsTechAdmin; }
        }

        public bool IsUseWorkDaysTimer
        {
            get { return m_IsUseWorkDaysTimer; }
        }

        public int TimeZoneOffset
        {
            get { return m_TimeZoneOffset; }
        }

        public string TimeZoneId
        {
            get { return m_TimeZoneId; }
        }

        public string FirstName
        {
            get { return m_FirstName; }
        }

        public string LastName
        {
            get { return m_LastName; }
        }

        public string FullName
        {
            get { return m_FirstName + " " + m_LastName; }
        }

        public string[] Roles
        {
            get { return m_Roles; }
        }
    }
}