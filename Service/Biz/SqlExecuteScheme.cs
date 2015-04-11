using System;
using System.Collections.Generic;
using System.Data;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using System.IO;
using System.Text;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class SqlExecuteScheme : IExecuteScheme
    {
        #region Max export excel row count
        //private static string maxRowCount = ConfigurationManager.AppSettings["MaxExptExcelCount"] != null ? ConfigurationManager.AppSettings["MaxExptExcelCount"] : "50000";
        private static string sheetName = "方案执行结果列表";
        #endregion
        private IConnection connection;
        public SqlExecuteScheme(IConnection connection)
        {
            this.connection = connection;
        }

        public string ExportExecuteResultToExcel(SchemeEntity entity)
        {
            try
            {
                string fileName = string.Format("{0}_{1}.csv", DateTime.Now.ToString("yyyyMMddHHmmss"), sheetName);
                DataSet dataSet = new DataSet();
                dataSet = this.connection.SearchDataSetForSql(entity.SQL_TEXT);
                
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    ExcelServer es = new ExcelServer();
                    string filePath = es.DataTableToCSV(dataSet.Tables[0], fileName);
                    Message msg = new Message();
                    string subject = string.Format("方案\"{0}\"在{1}执行出现异常情况", entity.SCHEME_NAME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    string body = string.Format("{0},请及时处理！详见附件", subject);
                    msg.sendMail(entity.NOTICE_TO, "emailforemail@163.com", subject, body, filePath);
                    return fileName;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        class StrPos
        {
            public StrPos(string _str, int _pos)
            {
                this.str = _str;
                this.pos = _pos;
            }
            public string str { get; set; }
            public int pos { get; set; }
        }
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public string ProcessData(string sqlText)
        {
            string newSql = sqlText.ToLower();
            int len;
            Stack<int> stack = new Stack<int>();
            Stack<StrPos> sp = new Stack<StrPos>();
            for (int i = 0; i < newSql.Length; i++)
            {
                if (newSql[i] == '(')
                {
                    stack.Push(i);
                }
                else if (newSql[i] == ')')
                {
                    int v = stack.Pop() + 1;
                    string subSql = newSql.Substring(v, i - v);
                    if (subSql.Contains("select"))
                    {
                        newSql = newSql.Remove(v, i - v);
                        i = v;
                        len = subSql.IndexOf(" where ");
                        if (len >= 0)
                        {
                            subSql = subSql.Substring(0, len + 7);
                            subSql += "1=2";
                        }
                        else subSql += " where 1=2";
                        StrPos val = new StrPos(subSql, v);
                        sp.Push(val);
                    }
                }
            }
            if (newSql.Contains("select"))
            {
                len = newSql.IndexOf(" where ");
                if (len >= 0)
                {
                    newSql = newSql.Substring(0, len + 7);
                    newSql += "1=2";
                }
                else newSql += " where 1=2";
            }
            foreach (StrPos item in sp)
            {
                newSql = newSql.Insert(item.pos, item.str);
            }
            return newSql;
        }
    }
}
