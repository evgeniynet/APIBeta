using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Level
    /// </summary>
    [DataContract(Name = "Level")]
    public class Level : ModelItemBase
    {
        public Level(DataRow row) : base(row) { }

        [DataMember(Name="name")]
        public string Name
        {
            get { return Row["LevelName"].ToString(); }
            set { Row["LevelName"] = value; }
        }

        [DataMember(Name="description")]
        public string Description
        {
            get { return Row["Description"].ToString(); }
            set { Row["Description"] = value; }
        }
    }
}