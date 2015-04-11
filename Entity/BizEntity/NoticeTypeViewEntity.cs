using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using HZAU.GP.DataMonitor.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HZAU.GP.DataMonitor.Entity.BizEntity
{
    /// <summary>
    /// 事件视图
    /// </summary>
    [Table(TableNames.NoticeTypeViewEntity)]
    [DataContract]
    public class NoticeTypeViewEntity
    {
        [DataMember]
        [Key]
        [Column("PK_ID")]
        public decimal? PK_ID { get; set; }

        [DataMember]
        [Column("NOTICE_TYPE_ID")]
        public string NOTICE_TYPE_ID { get; set; }

        [DataMember]
        [Column("NOTICE_TYPE_NAME")]
        public string NOTICE_TYPE_NAME { get; set; }

        [DataMember]
        [Column("NOTICE_TYPE_DESC")]
        public string NOTICE_TYPE_DESC { get; set; }

        [DataMember]
        public decimal? SORT_NUM { get; set; }
    }
}
