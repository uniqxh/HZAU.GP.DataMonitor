using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Entity.Database
{
    public enum CompareDesc
    {
        Equal,
        LargerEqual,
        LessEqual,
        Like,
        LikeAll
    }
    public enum BooleanExp
    {
        AND,
        OR
    }
    public class Sql
    {
        private StringBuilder sb;
        public Sql()
        {
            sb = new StringBuilder();
        }
        public Sql Where(string fieldname)
        {
            if(sb == null)
            {
                sb = new StringBuilder();
            }
            sb.AppendFormat(" 1=1 AND {0} ", fieldname);
            return this;
        }
        public Sql And(string fieldname)
        {
            sb.AppendFormat(" AND {0} ", fieldname);
            return this;
        }
        public Sql EqualTo(object value)
        {
            Type t = value.GetType();
            if(t == typeof(int) || t == typeof(decimal))
            {
                sb.AppendFormat(" = {0} ", value.ToString());
            }
            else
            {
                sb.AppendFormat(" = \'{0}\' ", value.ToString());
            }
            return this;
        }
        public string GetSql()
        {
            return sb.ToString();
        }
    }
}
