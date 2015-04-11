using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using HZAU.GP.DataMonitor.Entity.Resources;
using HZAU.GP.DataMonitor.Entity.DataModel;
using HZAU.GP.DataMonitor.Entity.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace HZAU.GP.DataMonitor.Entity.SearchCriteria
{
    public class SchemeSearchCriteria : SearchCriteriaModelBase
    {
        [DataMember]
        [Column("SCHEME_NAME")]
        [Display(ResourceType = typeof(DisplayNames), Name = "SCHEME_NAME")]
        [SearchBooleanExpAttributes(BooleanExp = BooleanExp.AND)]
        [SearchCompareDescAttributes(CompareDesc = CompareDesc.Equal)]
        public string SCHEME_NAME { get; set; }

        [DataMember]
        [Column("MODULE_ID")]
        [Display(ResourceType = typeof(DisplayNames), Name = "MODULE_ID")]
        [SearchBooleanExpAttributes(BooleanExp = BooleanExp.AND)]
        [SearchCompareDescAttributes(CompareDesc = CompareDesc.Equal)]
        public string MODULE_ID { get; set; }

        [DataMember]
        [Column("SQL_TYPE_ID")]
        [Display(ResourceType = typeof(DisplayNames), Name = "SQL_TYPE_ID")]
        [SearchBooleanExpAttributes(BooleanExp = BooleanExp.AND)]
        [SearchCompareDescAttributes(CompareDesc = CompareDesc.Equal)]
        public string SQL_TYPE_ID { get; set; }
    }
}
