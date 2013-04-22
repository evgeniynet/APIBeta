using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using bigWebApps.bigWebDesk;

namespace BWA.bigWebDesk.Api.Models
{
    [DataContract(Name = "Todos")]
    public class Todos : ModelItemCollectionGeneric<Todo>
    {
        public Todos(DataTable TodosTable) : base(TodosTable) { }

        public static List<Todo> GetTicketTodos(Guid organizationId, int departmentId, int ticketId, int projectId)
        {
            Todos _ticketTodos = new Todos(bigWebApps.bigWebDesk.Data.ToDo.SelectToDoListAndItems(organizationId, departmentId, ticketId, projectId));
            return _ticketTodos.Where(x => x.ItemType == 1 || x.ItemType == 2).ToList();
        }
    }
}