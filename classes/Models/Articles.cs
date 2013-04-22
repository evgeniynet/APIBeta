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
    [DataContract(Name = "Articles")]
    public class Articles : ModelItemCollectionGeneric<Article>
    {
        public Articles(DataTable ArticlesTable) : base(ArticlesTable) { }

        public static List<Article> GetArticles(Guid organizationId, int departmentId, int userId, int page, int limit)
        {
            Articles _articles = new Articles(bigWebApps.bigWebDesk.Data.KnowledgeBase.BrowseKnowledgeBase(organizationId, departmentId, false, 0, 0));
            return _articles.Skip(page * limit).Take(limit).ToList();
        }

        public static Ticket GetArticle(Guid organizationId, int departmentId, int articleId, Guid InstanceId)
        {
            Ticket article = new Ticket(organizationId, departmentId, articleId, InstanceId);
            return article;
        }
    }
}