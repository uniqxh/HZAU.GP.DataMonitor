using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using HZAU.GP.DataMonitor.Entity;
using HZAU.GP.DataMonitor.Entity.Resources;
namespace HZAU.GP.DataMonitor.Entity.BizEntity
{
    [Table(TableNames.SchemeViewEntity)]
    [DataContract]
    public class SchemeViewEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Key]
        [Column("PK_ID")]
        public decimal? PK_ID { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember]
        public DateTime? STAMP { get; set; }

        #region IIndex Members

        //[Ignore]
        public int Index
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 方案编号
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "SCHEME_ID")]
        [Column("SCHEME_ID")]
        public string SCHEME_ID { get; set; }
        /// <summary>
        /// 方案名称
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "SCHEME_NAME")]
        [Column("SCHEME_NAME")]
        public string SCHEME_NAME { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "MODULE_ID")]
        [Column("MODULE_ID")]
        public string MODULE_ID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [DataMember]
        //[Display(ResourceType = typeof(DisplayNames), Name = "MODULE_ID")]
        [Column("MODULE_NAME")]
        public string MODULE_NAME { get; set; }

        /// <summary>
        /// 脚本类型
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "SQL_TYPE_ID")]
        [Column("SQL_TYPE_ID")]
        public string SQL_TYPE_ID { get; set; }

        /// <summary>
        /// 脚本类型
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "SQL_TYPE_ID")]
        [Column("SQL_TYPE_NAME")]
        public string SQL_TYPE_NAME { get; set; }

        /// <summary>
        /// 脚本
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "SQL_TEXT")]
        [Column("SQL_TEXT")]
        public string SQL_TEXT { get; set; }

        /// <summary>
        /// 执行方式
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "EXECUTE_TYPE_ID")]
        [Column("EXECUTE_TYPE_ID")]
        public string EXECUTE_TYPE_ID { get; set; }

        /// <summary>
        /// 执行方式
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "EXECUTE_TYPE_ID")]
        [Column("EXECUTE_TYPE_NAME")]
        public string EXECUTE_TYPE_NAME { get; set; }

        /// <summary>
        /// 通知方式
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "NOTICE_TYPE_ID")]
        [Column("NOTICE_TYPE_ID")]
        public string NOTICE_TYPE_ID { get; set; }

        /// <summary>
        /// 通知方式
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "NOTICE_TYPE_ID")]
        [Column("NOTICE_TYPE_NAME")]
        public string NOTICE_TYPE_NAME { get; set; }

        /// <summary>
        /// 通知相关人
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "NOTICE_TO")]
        [Column("NOTICE_TO")]
        public string NOTICE_TO { get; set; }

        /// <summary>
        /// 执行周期
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "EXECUTE_CYCLE_ID")]
        [Column("EXECUTE_CYCLE_ID")]
        public string EXECUTE_CYCLE_ID { get; set; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "START_TIME")]
        [Column("START_TIME")]
        public DateTime? START_TIME { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "REMARK")]
        [Column("REMARK")]
        public string REMARK { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [Display(ResourceType = typeof(DisplayNames), Name = "STATUS")]
        [Column("STATUS")]
        public string STATUS { get; set; }

        /// <summary>
        /// 状态标识
        /// </summary>
        [DataMember]
        //[Display(ResourceType = typeof(DisplayNames),Name = "IS_EFFECTIVE")]
        [Column("IS_EFFECTIVE")]
        public decimal? IS_EFFECTIVE { get; set; }

        #region ICreatedBy Members

        [Display(ResourceType = typeof(DisplayNames), Name = "CreatedByName")]
        [DataMember]
        [Column(Fields.CreateByName)]
        //[ExcelExport(ColumnOrder = 10000)]
        public string CreatedByName { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "CREATE_DATE")]
        [DataMember]
        [Column(Fields.CreateDate)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        //[ExcelExport(ColumnOrder = 10001)]
        public DateTime CreationDate { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "CreatedBy")]
        [DataMember]
        [Column(Fields.CreateBy)]
        public int CreatedBy { get; set; }



        #endregion

        #region IUpdatedBy Members

        [Display(ResourceType = typeof(DisplayNames), Name = "LastUpdateByName")]
        [DataMember]
        [Column(Fields.UpdateByName)]
        //[ExcelExport(ColumnOrder = 10002)]
        public string LastUpdateByName { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "LAST_UPDATE_DATE")]
        [DataMember]
        [Column(Fields.UpdateDate)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        //[ExcelExport(ColumnOrder = 10003)]
        public DateTime? LastUpdateDate { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "LastUpdateBy")]
        [DataMember]
        [Column(Fields.UpdateBy)]
        public int? LastUpdateBy { get; set; }

        #endregion
    }
}
