using BWA.bigWebDesk.Api.Models;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using ServiceStack.Common.Web;
using System.Net;
using System.Data;
using System;

namespace BWA.bigWebDesk.Api.Services
{
    [Route("/freshbooks/clients", "GET, OPTIONS")]
    public class FB_Clients : PagedApiRequest
    {
    }

    [Route("/freshbooks/staff", "GET, OPTIONS")]
    public class FB_Staff : PagedApiRequest
    {
    }

    [Route("/freshbooks/projects", "GET, OPTIONS")]
    public class FB_Projects : PagedApiRequest
    {
        public string client { get; set; }
        public string staff { get; set; }
    }

    [Route("/freshbooks/tasks", "GET, OPTIONS")]
    public class FB_Tasks : PagedApiRequest
    {
        public string project { get; set; }
    }

    [Route("/freshbooks", "POST, OPTIONS")]
    public class FB_Data : ApiRequest
    {
        public int user_id { get; set; }
        public int fb_staff_id { get; set; }
        public int account_id { get; set; }
        public int fb_client_id { get; set; }
        public int project_id { get; set; }
        public int fb_project_id { get; set; }
        public int task_type_id { get; set; }
        public int fb_task_type_id { get; set; }
    }

    [Route("/freshbooks/time", "POST, OPTIONS")]
    public class FB_Time : ApiRequest
    {
        public int? fb_staff_id { get; set; }
        public int fb_project_id { get; set; }
        public int fb_task_type_id { get; set; }
        public decimal hours { get; set; }
        public string notes { get; set; }
        public DateTime? date { get; set; }
        public int time_id { get; set; }
        public bool? is_project_log { get; set; }
    }

    public class FreshBooksServices : Service
    {
        [Secure()]
        public object Get(FB_Clients request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);

            return FBClients.GetFBClients(instanceConfig, request.page, request.limit);
        }

        [Secure()]
        public object Get(FB_Staff request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);

            return FBStaffs.GetFBStaff(instanceConfig, request.page, request.limit);
        }

        [Secure()]
        public object Get(FB_Projects request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);

            int clientID = 0;
            if (!int.TryParse(request.client, out clientID))
            {
                throw new HttpError(HttpStatusCode.NotFound, "incorrect client id");
            }
            int staffID = 0;
            if (!int.TryParse(request.staff, out staffID))
            {
                throw new HttpError(HttpStatusCode.NotFound, "incorrect staff id");
            }

            return FBProjects.GetFBProjects(instanceConfig, request.page, request.limit, clientID, staffID);
        }

        [Secure()]
        public object Get(FB_Tasks request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);

            int projectID = 0;
            if (!int.TryParse(request.project, out projectID))
            {
                throw new HttpError(HttpStatusCode.NotFound, "incorrect project id");
            }

            return FBTasks.GetFBTasks(instanceConfig, request.page, request.limit, projectID);
        }

        [Secure()]
        public object Post(FB_Data request)
        {
            ApiUser hdUser = request.ApiUser;
            try
            {
                FBModel.UpdateData(hdUser, request.user_id, request.fb_staff_id, request.account_id, request.fb_client_id,
                    request.project_id, request.fb_project_id, request.task_type_id, request.fb_task_type_id);
            }
            catch (Exception ex)
            {
                throw new HttpError(ex.Message.Replace("\n", " ").Replace("\r", " "));
            }
            return new HttpResult("", HttpStatusCode.OK);
        }

        [Secure()]
        public object Post(FB_Time request)
        {
            ApiUser hdUser = request.ApiUser;
            Instance_Config instanceConfig = new Models.Instance_Config(hdUser);
            bool isProjectLog = false;
            string notes = "";
            int staffID = 0;
            DateTime date = DateTime.MinValue;
            if (request.is_project_log.HasValue)
            {
                isProjectLog = request.is_project_log.Value;
            }
            if (request.notes != null)
            {
                notes = request.notes;
            }
            if (request.fb_staff_id.HasValue)
            {
                staffID = request.fb_staff_id.Value;
            }
            if (request.date.HasValue)
            {
                date = request.date.Value;
            }
            try
            {
                string result = FBTimeEntries.CreateTimeEntry(hdUser, instanceConfig, staffID, request.fb_project_id, request.fb_task_type_id,
                    request.hours, notes, date, request.time_id, isProjectLog);
                if (result == "ok")
                {
                    return new HttpResult("", HttpStatusCode.OK);
                }
                else
                {
                    throw new HttpError(result.Replace("\n", " ").Replace("\r", " "));
                }
            }
            catch (Exception ex)
            {
                throw new HttpError(ex.Message.Replace("\n", " ").Replace("\r", " "));
            }
        }
    }
}