using HZAU.GP.DataMonitor.Entity.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HZAU.GP.DataMonitor.Entity.DataModel
{
    public abstract class SearchCriteriaModelBase
    {
        private static Dictionary<string, List<PropertyInfo>> propertyInfoCache = new Dictionary<string, List<PropertyInfo>>();
        private static object lockObj = new object();
        public SearchCriteriaModelBase() { }
        public virtual string GetSql()
        {
            try
            {
                Sql sql = new Sql();
                var properties = this.GetAllPropertyInfo();
                string itemStrFormat = " {0} {1} {2} {3} ";
                StringBuilder sb = new StringBuilder("1=1 ");
                foreach (var pi in properties)
                {
                    string booleanStr = this.GetBooleanStr(pi);
                    CompareDesc compareDesc = this.GetCompareStr(pi);
                    string compareStr = this.ParseCompare(compareDesc);
                    var colName = pi.Name;
                    var value = pi.GetValue(this);
                    if (value != null)
                    {
                        Type t = pi.GetType();
                        if(t != typeof(int) && t != typeof(decimal))
                        {
                            value = string.Format("\'{0}\'", value.ToString());
                        }
                        sb.AppendFormat(itemStrFormat, booleanStr, colName, compareStr, value);
                    }
                }
                string selectStr = sb.ToString();
                return selectStr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string ParseCompare(CompareDesc compare)
        {
            switch (compare)
            {
                case CompareDesc.Equal:
                    return "=";
                case CompareDesc.LargerEqual:
                    return ">=";
                case CompareDesc.LessEqual:
                    return "<=";
                case CompareDesc.Like:
                    return " LIKE ";
                case CompareDesc.LikeAll:
                    return " LIKE ";
            }
            throw new ArgumentException();
        }

        private CompareDesc GetCompareStr(PropertyInfo pi)
        {
            CompareDesc compareDesc = CompareDesc.Equal;
            if (pi.IsDefined(typeof(SearchCompareDescAttributes), true))
            {
                object[] obj = pi.GetCustomAttributes(typeof(SearchCompareDescAttributes), true);
                if (obj.Count() > 0)
                {
                    SearchCompareDescAttributes compareDescAttr = obj[0] as SearchCompareDescAttributes;
                    compareDesc = compareDescAttr.CompareDesc;
                }
            }
            return compareDesc;
        }

        private string GetBooleanStr(PropertyInfo pi)
        {
            string booleanExpStr = "AND";
            if (pi.IsDefined(typeof(SearchBooleanExpAttributes), true))
            {
                object[] obj = pi.GetCustomAttributes(typeof(SearchBooleanExpAttributes), true);
                if (obj.Count() > 0)
                {
                    SearchBooleanExpAttributes booleanExp = obj[0] as SearchBooleanExpAttributes;
                    booleanExpStr = booleanExp.BooleanExp.ToString();
                }
            }
            return booleanExpStr;
        }

        protected List<PropertyInfo> GetAllPropertyInfo()
        {
            Type type = this.GetType();
            string typename = type.FullName;
            try
            {
                EnsureCacheType(type);
                List<PropertyInfo> listPi = null;
                propertyInfoCache.TryGetValue(typename, out listPi);
                return listPi;
            }
            catch(Exception)
            {
                return null;
            }
        }
        private static void EnsureCacheType(Type type)
        {
            string typeName = type.FullName;
            if(!propertyInfoCache.ContainsKey(typeName))
            {
                lock (lockObj)
                {
                    if (!propertyInfoCache.ContainsKey(typeName))
                    {
                        List<PropertyInfo> pis = FetchAllPropertyInfoType(type);
                        propertyInfoCache.Add(typeName, pis);
                    }
                }
            }
        }

        private static List<PropertyInfo> FetchAllPropertyInfoType(Type type)
        {
            List<PropertyInfo> pis = type.GetProperties().ToList();
            return pis;
        }
    }
}
