using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZAU.GP.DataMonitor.Entity.Database;

namespace HZAU.GP.DataMonitor.Entity.DataModel
{
    public class SearchBooleanExpAttributes : Attribute
    {
        public BooleanExp BooleanExp { get; set; }
    }
    public class SearchCompareDescAttributes:Attribute
    {
        public CompareDesc CompareDesc { get; set; }
    }
}
