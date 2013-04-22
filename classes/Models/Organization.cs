using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using ServiceStack.ServiceInterface;

namespace BWA.bigWebDesk.Api.Models
{
    /// <summary>
    /// Summary description for Ticket
    /// </summary>
    [DataContract(Name = "Organization")]
    public class Organization
    {
        readonly Micajah.Common.Bll.Organization _innerOrganization;

        public Organization(Micajah.Common.Bll.Organization org, Micajah.Common.Bll.InstanceCollection insts)
        {
            _innerOrganization = org;
            Instances=new List<Instance>(insts.Count);
            foreach (Micajah.Common.Bll.Instance inst in insts) Instances.Add(new Instance(inst));
        }

        //[DataMember]
        public Guid Id
        {
            get { return _innerOrganization.OrganizationId; }
            set { _innerOrganization.OrganizationId = value; }
        }

        [DataMember(Name = "key")]
        public string Key
        {
            get { return _innerOrganization.PseudoId; }
            set { _innerOrganization.PseudoId = value; }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return _innerOrganization.Name; }
            set { _innerOrganization.Name = value; }
        }

        [DataMember(Name = "instances")]
        public List<Instance> Instances { get; set; }
    }
}