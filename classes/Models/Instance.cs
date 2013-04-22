using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Ticket
    /// </summary>
    [DataContract(Name = "Instance")]
    public class Instance
    {
        protected Micajah.Common.Bll.Instance InnerInstance;

        public Instance(Micajah.Common.Bll.Instance inst)
        {
            InnerInstance = inst;
        }

        //[DataMember]
        public Guid Id
        {
            get { return InnerInstance.InstanceId; }
            set { InnerInstance.InstanceId = value; }
        }

        [DataMember(Name = "key")]
        public string Key
        {
            get { return InnerInstance.PseudoId; }
            set { InnerInstance.PseudoId = value; }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return InnerInstance.Name; }
            set { InnerInstance.Name = value; }
        }
    }
}