using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class ExcelServer : IExcelServer
    {
        #region 导出Excel
        //private Microsoft.Office.Interop.Excel.Application excel;
        //private Microsoft.Office.Interop.Excel._Workbook workBook;
        //private Microsoft.Office.Interop.Excel._Worksheet workSheet;
        //private object misValue;
        //public ExcelServer()
        //{
        //    excel = new Microsoft.Office.Interop.Excel.Application();
        //    misValue = System.Reflection.Missing.Value;
        //    workBook = excel.Workbooks.Add(misValue);
        //    workSheet = (Microsoft.Office.Interop.Excel._Worksheet)workBook.ActiveSheet;
        //}
        //public string DataSetToExcel(DataSet dataSet, string FileName, string sheetName)
        //{
        //    DataTable dataTable = dataSet.Tables[0];
        //    //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        //    //Microsoft.Office.Interop.Excel._Workbook workBook;
        //    //Microsoft.Office.Interop.Excel._Worksheet workSheet;
        //    //object misValue = System.Reflection.Missing.Value;
        //    //workBook = excel.Workbooks.Add(misValue);
        //    //workSheet = (Microsoft.Office.Interop.Excel._Worksheet)workBook.ActiveSheet;
        //    try
        //    { 
        //        workSheet.Name = sheetName;
        //        int rowIndex = 1;
        //        int colIndex = 0;
        //        //取得标题
        //        foreach (DataColumn col in dataTable.Columns)
        //        {
        //            colIndex++;

        //            excel.Cells[1, colIndex] = col.ColumnName;
        //        }

        //        //取得表格中的数据
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            rowIndex++;

        //            colIndex = 0;

        //            foreach (DataColumn col in dataTable.Columns)
        //            {
        //                colIndex++;

        //                excel.Cells[rowIndex, colIndex] =

        //                     row[col.ColumnName].ToString().Trim();

        //                //设置表格内容居中对齐
        //                workSheet.Range[excel.Cells[rowIndex, colIndex],

        //                  excel.Cells[rowIndex, colIndex]].HorizontalAlignment =

        //                  Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        //            }
        //        }

        //        excel.Visible = false;
        //        string filepath = string.Format("{0}\\HZAU.GP.DataMonitor.Web\\Upload\\{1}", GetBaseFilePath(), FileName);
        //        workBook.SaveAs(filepath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue,
        //            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
        //            misValue, misValue, misValue, misValue, misValue);
        //        dataTable = null;
        //        return filepath;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        workBook.Close(true, misValue, misValue);
        //        excel.Quit();
        //        PublicMethod.Kill(excel);//调用kill当前excel进程
        //        releaseObject(workSheet);
        //        releaseObject(workBook);
        //        releaseObject(excel);
        //    }
        //}
        //private static void releaseObject(object obj)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch
        //    {
        //        obj = null;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
        #endregion
        private string GetBaseFilePath()
        {
            //只能先得到debug的目录
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            //去掉debug,bin
            string rootPath = basePath.Substring(0, basePath.LastIndexOf("\\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            return rootPath;
        }

        public string DataTableToCSV(DataTable dt, string FileName)
        {
            string filepath = string.Format("{0}\\HZAU.GP.DataMonitor.Web\\Upload\\{1}", GetBaseFilePath(), FileName);
            FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Clear();
                foreach(DataColumn column in dt.Columns)
                {
                    sb.Append(column.ColumnName);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                sw.WriteLine(sb);
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    sb.Clear();
                    for (int j = 0; j < dt.Columns.Count; ++j)
                    {
                        sb.Append(dt.Rows[i][j].ToString());
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sw.WriteLine(sb);
                }
                return filepath;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
    }

    //public class PublicMethod
    //{
    //    [DllImport("User32.dll", CharSet = CharSet.Auto)]

    //    public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

    //    public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
    //    {
    //        try
    //        {
    //            IntPtr t = new IntPtr(excel.Hwnd);

    //            int k = 0;

    //            GetWindowThreadProcessId(t, out k);

    //            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);

    //            p.Kill();
    //        }
    //        catch
    //        { }
    //    }
    //}
}
