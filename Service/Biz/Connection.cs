using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Data;
using HZAU.GP.DataMonitor.Entity.BizEntity;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class Connection : IConnection
    {
        private OracleConnection con;
        private const string oradb = "data source=ORCL;User Id=C##TEST;Password=root;";
        public Connection()
        {
            con = new OracleConnection(oradb);
        }
        /// <summary>
        /// 数据库查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Select<T>(string sql) where T: new()
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                string tablename = this.GetTableName<T>();
                if (string.IsNullOrEmpty(tablename))
                {
                    throw new Exception("实体表名为空！");
                }
                cmd.CommandText = string.Format("select * from {0} where {1} order by PK_ID desc ", tablename, sql);
                OracleDataReader odr = cmd.ExecuteReader();
                List<T> result = new List<T>();
                while (odr.Read())
                {
                    T entity = new T();
                    int count = odr.FieldCount;
                    for (int i = 0; i < count;++i )
                    {
                        string name = odr.GetName(i);
                        var m = entity.GetType().GetProperty(name);
                        if (m != null && !odr.IsDBNull(i))
                        {
                            Type t = m.PropertyType;
                            if (t == typeof(int))
                            {
                                m.SetValue(entity, odr.GetInt32(i));
                            }
                            else if (t == typeof(decimal) || t == typeof(decimal?))
                            {
                                m.SetValue(entity, odr.GetDecimal(i));
                            }
                            else if (t == typeof(string))
                            {
                                m.SetValue(entity, odr.GetString(i));
                            }
                            else if (t == typeof(DateTime) || t == typeof(DateTime?))
                            {
                                m.SetValue(entity, odr.GetDateTime(i));
                            }
                        }
                    }
                    result.Add(entity);
                }
                odr.Close();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 数据库插入操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Insert<T>(T entity)
        {
            try
            {
                string sql = this.GetInsertSqlString<T>(entity);
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = sql;
                int ret = cmd.ExecuteNonQuery();
                if(ret != 1)
                {
                    throw new Exception("数据插入异常");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public void Update<T>(T entity)
        {
            try
            {
                string sql = this.GetUpdateSqlString<T>(entity);
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = sql;
                int ret = cmd.ExecuteNonQuery();
                if(ret != 1)
                {
                    throw new Exception("更新数据异常");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "commit";
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public DataSet SearchDataSetForSql(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                OracleDataAdapter od = new OracleDataAdapter(sql, con);
                od.Fill(ds);
                return ds;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public string SearchDataSetForProcedure(SchemeEntity entity)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = entity.SQL_TEXT;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter param = new OracleParameter();
                param = cmd.Parameters.Add("v_toMail", OracleType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = entity.NOTICE_TO;
                string subject = string.Format("方案\"{0}\"在{1}执行出现异常情况", entity.SCHEME_NAME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                param = cmd.Parameters.Add("v_subject", OracleType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = subject;
                param = cmd.Parameters.Add("v_body", OracleType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = string.Format("{0},请及时处理！详见附件", subject);
                int cnt = 0;
                string fileName = string.Empty;
                cmd.Parameters.Add("cnt", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("fileName", OracleType.VarChar, 255).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cnt = Convert.ToInt32(cmd.Parameters["cnt"].Value);
                fileName = cmd.Parameters["fileName"].Value.ToString();
                if(cnt == 0) fileName = string.Empty;
                return fileName;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 获得插入sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetInsertSqlString<T>(T entity)
        {
            string sqlName = string.Empty;
            string sqlValue = string.Empty;
            string tableName = this.GetTableName<T>();
            Type type = typeof(T);
            var pis = type.GetProperties();
            foreach(var pi in pis)
            {
                string name = pi.Name;
                var value = pi.GetValue(entity);
                if(value != null)
                {
                    sqlName = string.Format("{0},{1}", sqlName, name);
                    if(name == "STAMP")
                    {
                        sqlValue = string.Format("{0},to_timestamp(\'{1}\','yyyy-mm-dd hh24:mi:ss.ff')", sqlValue, value);
                        continue;
                    }
                    Type t = pi.PropertyType;
                    if(t == typeof(int) || t == typeof(decimal) || t == typeof(decimal?))
                    {
                        sqlValue = string.Format("{0},{1}", sqlValue, value);
                    }
                    else if( t == typeof(DateTime) || t == typeof(DateTime?))
                    {
                        sqlValue = string.Format("{0},to_date(\'{1}\','yyyy-mm-dd hh24:mi:ss')", sqlValue, value);
                    }
                    else
                    {
                        sqlValue = string.Format("{0},\'{1}\'", sqlValue, value);
                    }
                }
            }
            sqlName = sqlName.Trim(',');
            sqlValue = sqlValue.Trim(',');
            return string.Format("Insert into {0} ( {1} ) values ( {2} )", tableName, sqlName, sqlValue);
        }
        private string GetUpdateSqlString<T>(T entity)
        {
            string setPart = string.Empty;
            string pkId = string.Empty;
            string tableName = this.GetTableName<T>();
            Type type = typeof(T);
            PropertyInfo[] pis = type.GetProperties();
            foreach(PropertyInfo pi in pis)
            {
                string name = pi.Name;
                var value = pi.GetValue(entity);
                if(value != null)
                {
                    if(name == "PK_ID")
                    {
                        pkId = value.ToString();
                        continue;
                    }
                    if (name == "STAMP")
                    {
                        setPart = string.Format("{0}, {1} = to_timestamp(\'{2}\','yyyy-mm-dd hh24:mi:ss.ff')", setPart, name, value.ToString());
                        continue;
                    }
                    Type t = pi.PropertyType;
                    if(t == typeof(int) || t == typeof(decimal) || t == typeof(decimal?))
                    {
                        setPart = string.Format("{0}, {1} = {2}", setPart, name, value.ToString());
                    }
                    else if (t == typeof(DateTime) || t == typeof(DateTime?))
                    {
                        setPart = string.Format("{0}, {1} = to_date(\'{2}\','yyyy-mm-dd hh24:mi:ss')", setPart, name, value.ToString());
                    }
                    else
                    {
                        setPart = string.Format("{0}, {1} = \'{2}\'", setPart, name, value.ToString());
                    }
                }
            }
            setPart = setPart.Trim(',');
            return string.Format("Update {0} set {1} where PK_ID = {2}", tableName, setPart, pkId);
        }
        /// <summary>
        /// 获得表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetTableName<T>()
        {
            string tablename = string.Empty;
            Type type = typeof(T);
            object[] objs = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (objs != null)
            {
                TableAttribute attr = objs[0] as TableAttribute;
                tablename = attr.Name;
            }
            if (string.IsNullOrEmpty(tablename))
            {
                tablename = type.Name;
            }
            return tablename;
        }
    }
}
