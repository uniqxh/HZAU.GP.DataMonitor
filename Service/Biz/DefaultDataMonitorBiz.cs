using System;
using System.Collections.Generic;
using System.Linq;
using HZAU.GP.DataMonitor.Entity;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using System.Data;
using HZAU.GP.DataMonitor.Entity.Resources;
using HZAU.GP.DataMonitor.Service;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class DefaultDataMonitorBiz:IDataMonitorBiz
    {
        //private IRepository repository;
        //private IRightBiz rightBiz;
        //private IPeriodBiz periodBiz;
        //private IDictBiz dictBiz;
        private ICreateDataMonitorBiz createSchemeBiz;
        private IGetDataMonitorBiz getSchemeBiz;
        private IUpdateSchemeBiz updateSchemeBiz;
        //private SubmitAction currentAction;
        //public SubmitAction CurrentAction
        //{
        //    get
        //    {
        //        return this.currentAction;
        //    }
        //    set
        //    {
        //        this.currentAction = value;
        //        this.dictBiz.CurrentAction = value;
        //        this.createSchemeBiz.CurrentAction = value;
        //        this.getSchemeBiz.CurrentAction = value;
        //        this.updateSchemeBiz.CurrentAction = value;
        //    }
        //}
        public DefaultDataMonitorBiz(ICreateDataMonitorBiz createSchemeBiz, 
            IGetDataMonitorBiz getSchemeBiz, IUpdateSchemeBiz updateSchemeBiz)
        {
            //this.repository = repository;
            this.createSchemeBiz = createSchemeBiz;
            this.getSchemeBiz = getSchemeBiz;
            this.updateSchemeBiz = updateSchemeBiz;
        }
        
        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="SchemeEntity"></param>
        /// <returns></returns>
        public bool InsertScheme(SchemeEntity SchemeEntity)
        {
            return this.createSchemeBiz.InsertScheme(SchemeEntity);
        }

        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria)
        {
            return this.getSchemeBiz.SearchScheme(criteria);
        }

        /// <summary>
        /// 导出方案的执行脚本数据到excel
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public string ExportSchemeToExcel(int pkId)
        {
            return this.getSchemeBiz.ExportSchemeToExcel(pkId);
        }
        public bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp)
        {
            return this.updateSchemeBiz.SetSchemeEffective(pkId, isEffective, stamp);
        }
    }
}
