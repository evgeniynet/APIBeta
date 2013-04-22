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
    [DataContract(Name = "Asset")]
    public class Asset : ModelItemBase
    {
        public Asset(DataRow row) : base(row) { }

        [DataMember(Name = "serial_tag_number")]
        public string SerialTagNumber
        {
            get { return Row["SerialTagNumber"].ToString(); }
            set { Row["SerialTagNumber"] = value; }
        }

        [DataMember(Name = "category")]
        public string CategoryName
        {
            get { return Row["AssetCategoryName"].ToString(); }
            set { Row["AssetCategoryName"] = value; }
        }

        [DataMember(Name = "type")]
        public string TypeName
        {
            get { return Row["AssetTypeName"].ToString(); }
            set { Row["AssetTypeName"] = value; }
        }

        [DataMember(Name = "make")]
        public string MakeName
        {
            get { return Row["AssetMakeName"].ToString(); }
            set { Row["AssetMakeName"] = value; }
        }

        [DataMember(Name = "model")]
        public string ModelName
        {
            get { return Row["AssetModelName"].ToString(); }
            set { Row["AssetModelName"] = value; }
        }

        [DataMember(Name = "serial")]
        public string SerialNumber
        {
            get { return Row["SerialNumber"].ToString(); }
            set { Row["SerialNumber"] = value; }
        }

        [DataMember(Name = "unique1")]
        public string Unique1
        {
            get { return Row["Unique1"].ToString(); }
            set { Row["Unique1"] = value; }
        }

        [DataMember(Name = "unique2")]
        public string Unique2
        {
            get { return Row["Unique2"].ToString(); }
            set { Row["Unique2"] = value; }
        }

        [DataMember(Name = "unique3")]
        public string Unique3
        {
            get { return Row["Unique3"].ToString(); }
            set { Row["Unique3"] = value; }
        }

        [DataMember(Name = "unique4")]
        public string Unique4
        {
            get { return Row["Unique4"].ToString(); }
            set { Row["Unique4"] = value; }
        }

        [DataMember(Name = "unique5")]
        public string Unique5
        {
            get { return Row["Unique5"].ToString(); }
            set { Row["Unique5"] = value; }
        }

        [DataMember(Name = "unique6")]
        public string Unique6
        {
            get { return Row["Unique6"].ToString(); }
            set { Row["Unique6"] = value; }
        }

        [DataMember(Name = "unique7")]
        public string Unique7
        {
            get { return Row["Unique7"].ToString(); }
            set { Row["Unique7"] = value; }
        }
    }
}