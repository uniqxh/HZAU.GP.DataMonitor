using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.BizEntity;

namespace HZAU.GP.DataMonitor.Entity.SearchResult
{
    [DataContract]
    public class SchemeSearchResult
    {
        [DataMember]
        public SchemeSearchCriteria SchemeSearchCriteria { get; set; }

        [DataMember]
        public List<SchemeEntity> SchemeList { get; set; }
    }
}
