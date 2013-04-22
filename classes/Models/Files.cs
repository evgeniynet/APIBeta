using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;
using Micajah.FileService.Client.Dal;
using Micajah.FileService.Client.Dal.MetaDataSetTableAdapters;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for TicketLogRecords
    /// </summary>
    [DataContract(Name = "Files")]
    public class Files : ModelItemCollectionGeneric<File>
    {      
        public Files(DataTable FilesTable) : base(FilesTable) { }

        public static List<File> GetFiles(Guid organizationId, Guid instanceId, int ticketId)
        {
            MetaDataSet.FileDataTable tktFiles = null;
            FileTableAdapter fta = new FileTableAdapter(bigWebApps.bigWebDesk.Data.DBAccess.GetConnectionString(organizationId));
            tktFiles = fta.GetFiles(instanceId, instanceId, "tickets-tickets-files", ticketId.ToString(), false);
            if (tktFiles != null && tktFiles.Count > 0)
            {
                List<File> files = new Files(tktFiles).List;
                foreach (File file in files)
                {
                    string fileUrl = Micajah.FileService.Client.Access.GetFileUrl(file.FileUniqueId,
                                                                                    file.OrganizationId,
                                                                                    file.DepartmentId);
                    if (!string.IsNullOrEmpty(fileUrl))
                    { //fileUrl = "https://files.bigwebapps.com" + fileUrl.Substring(fileUrl.IndexOf("/File.ashx"));
                        file.Url = fileUrl;
                    }
                }
                return files;
            }
            return null;
        }
    }
}