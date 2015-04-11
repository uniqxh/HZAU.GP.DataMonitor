
namespace HZAU.GP.DataMonitor.Service.Biz
{
    class DefaultExecuteSchemeProvider : IExecuteSchemeProvider
    {
        private const string ExecuteByProc = "PROCEDURE";
        private const string ExecuteBySql = "SQL";
        public IExecuteScheme GetSchemeExecuteType(string executeTypeId, IConnection con)
        {
            switch (executeTypeId.ToUpper())
            {
                case ExecuteByProc:
                    return new ProcedureExecuteScheme(con);
                case ExecuteBySql:
                    return new SqlExecuteScheme(con);
                default:
                    return new SqlExecuteScheme(con);
            }
        }
    }
}
