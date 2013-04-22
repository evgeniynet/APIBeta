using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Class
    /// </summary>
    [DataContract(Name = "User_Config")]
    public class UserConfig
    {
        public UserConfig(ApiUser usr)
        {
            LoginId = usr.LoginId;
            UserId = usr.UserId;
            Email = usr.LoginEmail;
            FirstName = usr.FirstName;
            LastName = usr.LastName;
            IsTechOrAdmin = usr.IsTechAdmin;
            IsUseWorkDaysTimer = usr.IsUseWorkDaysTimer;
        }

        [DataMember(Name = "login_id")]
        public Guid LoginId { get; set; }

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "firstname")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastname")]
        public string LastName { get; set; }

        [DataMember(Name = "is_techoradmin")]
        public bool IsTechOrAdmin { get; set; }

        [DataMember(Name = "is_useworkdaystimer")]
        public bool IsUseWorkDaysTimer { get; set; }
    }
}