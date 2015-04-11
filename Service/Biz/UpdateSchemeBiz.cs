using System;
using System.Collections.Generic;
using System.Linq;
using HZAU.GP.DataMonitor.Entity;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using System.Data;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class UpdateSchemeBiz : IUpdateSchemeBiz
    {
        private IConnection connection;
        private IGetDataMonitorBiz _getDataMonitorBiz;
        //private IUpdateByPreparation updateByPreparation;
        //private SubmitAction _submitAction;
        //public SubmitAction CurrentAction
        //{
        //    get
        //    {
        //        return this._submitAction;
        //    }
        //    set
        //    {
        //        this._submitAction = value;
        //        this._getDataMonitorBiz.CurrentAction = value;
        //    }
        //}

        public UpdateSchemeBiz(IGetDataMonitorBiz getDataMonitorBiz, IConnection connection)
        {
           // this._repository = repository;
            this._getDataMonitorBiz = getDataMonitorBiz;
            this.connection = connection;
        }

        /// <summary>
        /// 改变方案有效状态
        /// </summary>
        /// <param name="isEffective"></param>
        /// <returns></returns>
        public bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp)
        {
            try
            {
                var entity = this._getDataMonitorBiz.GetSchemeEntityById(pkId);
                entity.STAMP = stamp;
                entity.IS_EFFECTIVE = isEffective;
                this.connection.Update<SchemeEntity>(entity);
                this.connection.Commit();
                //if (isEffective == "0")
                //{
                //    entity.IS_EFFECTIVE = 0M;
                //}
                //else
                //{
                //    entity.IS_EFFECTIVE = 1M;
                //}
                //this.updateByPreparation.Prepare(entity, this.CurrentAction.ActionBy.CurrentUser);
                //this._repository.DataOperationRepository.Update<SchemeEntity>(entity);
                //this._repository.DataOperationRepository.Commit();
                return true;
            }
            catch (Exception e)
            {
                //throw(e);
                return false;
            }
        }
    }
}
