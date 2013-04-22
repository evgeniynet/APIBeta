using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for ModelItemBase
    /// </summary>
    public interface ModelItemBaseInterface
    {
        DataRow Row {get;}
        int Id { get; set; }
    }
}