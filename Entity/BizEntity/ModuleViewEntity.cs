using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using HZAU.GP.DataMonitor.Entity;

namespace HZAU.GP.DataMonitor.Entity.BizEntity
{
    /// <summary>
    /// 事件视图
    /// </summary>
    [Table(TableNames.ModuleViewEntity)]
    [DataContract]
    public class ModuleViewEntity
    {
        [DataMember]
        [Key]
        [Column("PK_ID")]
        public decimal? PK_ID { get; set; }

        [DataMember]
        [Column("MODULE_ID")]
        public string MODULE_ID { get; set; }

        [DataMember]
        [Column("MODULE_NAME")]
        public string MODULE_NAME { get; set; }

        [DataMember]
        [Column("MODULE_DESC")]
        public string MODULE_DESC { get; set; }

        [DataMember]
        public decimal? SORT_NUM { get; set; }
    }
}
