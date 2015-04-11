using System;
using System.Data;
using HZAU.GP.DataMonitor.Entity.BizEntity;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class ProcedureExecuteScheme : IExecuteScheme
    {
        #region Max export excel row count
        private static string sheetName = "方案执行结果列表";
        #endregion
        private IConnection connection;
        public ProcedureExecuteScheme(IConnection connection)
        {
            this.connection = connection;
        }

        public string ExportExecuteResultToExcel(SchemeEntity entity)
        {
            try
            {
                string fileName = this.connection.SearchDataSetForProcedure(entity);
                //ExcelServer es = new ExcelServer();
                //string filePath = es.DataSetToExcel(dataSet, fileName, sheetName);
                //if (dataSet.Tables[0].Columns.Count > 0)
                //{
                //    Message msg = new Message();
                //    string subject = string.Format("方案{0}执行出现异常情况", entity.SCHEME_NAME);
                //    string body = string.Format("{0},请及时处理！详见附件", subject);
                //    msg.sendMail(entity.NOTICE_TO, "525799145@qq.com", subject, body, filePath);
                //}
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
