using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IExcelServer
    {
        //string DataSetToExcel(DataSet ds, string FileName, string sheetName);

        string DataTableToCSV(DataTable dt, string FileName);
    }
}
