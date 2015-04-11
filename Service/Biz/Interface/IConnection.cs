using HZAU.GP.DataMonitor.Entity.BizEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IConnection
    {
        List<T> Select<T>(string sql) where T : new();
        void Insert<T>(T entity);
        void Update<T>(T entity);
        void Commit();
        DataSet SearchDataSetForSql(string sql);
        string SearchDataSetForProcedure(SchemeEntity entity);
    }
}
