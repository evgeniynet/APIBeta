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
    [DataContract(Name = "Assets_Config")]
    public class AssetsConfig
    {
        public AssetsConfig(bigWebApps.bigWebDesk.AssetsConfig assetsCfg)
        {
            Unique1Caption = assetsCfg.Unique1Caption;
            Unique2Caption = assetsCfg.Unique2Caption;
            Unique3Caption = assetsCfg.Unique3Caption;
            Unique4Caption = assetsCfg.Unique4Caption;
            Unique5Caption = assetsCfg.Unique5Caption;
            Unique6Caption = assetsCfg.Unique6Caption;
            Unique7Caption = assetsCfg.Unique7Caption;
        }

        [DataMember(Name = "unique1_caption")]
        public string Unique1Caption
        { get; set; }

        [DataMember(Name = "unique2_caption")]
        public string Unique2Caption
        { get; set; }

        [DataMember(Name = "unique3_caption")]
        public string Unique3Caption
        { get; set; }

        [DataMember(Name = "unique4_caption")]
        public string Unique4Caption
        { get; set; }

        [DataMember(Name = "unique5_caption")]
        public string Unique5Caption
        { get; set; }

        [DataMember(Name = "unique6_caption")]
        public string Unique6Caption
        { get; set; }

        [DataMember(Name = "unique7_caption")]
        public string Unique7Caption
        { get; set; }
    }
}