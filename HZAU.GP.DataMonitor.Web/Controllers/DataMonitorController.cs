using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HZAU.GP.DataMonitor.Web.Models;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.BizEntity;

namespace HZAU.GP.DataMonitor.Web.Controllers
{
    public class DataMonitorController : Controller
    {
        //
        // GET: /DataMonitor/
        IDataMonitorModel dataMonitorModel;
        public DataMonitorController(IDataMonitorModel model)
        {
            this.dataMonitorModel = model;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCheckScheme()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxAddScheme(SchemeEntity entity)
        {
            try
            {
                this.dataMonitorModel.InsertScheme(entity);
                return Json(new { message = "OK"});
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult ExecuteCheckScheme()
        {
            return View();
        }
        /// <summary>
        /// 执行检查方案
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxExecuteCheckScheme(SchemeSearchCriteria SchemeSearchCriteria)
        {
            try
            {
                var searchResult = this.dataMonitorModel.SearchScheme(SchemeSearchCriteria);
                return Json(searchResult.SchemeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult AjaxSetSchemeEffective(int pkId, int isEffective, DateTime stamp)
        {
            try
            {
                this.dataMonitorModel.SetSchemeEffective(pkId, isEffective, stamp);
                return Json(new { message = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult AjaxExportSchemeToExcel(int pkId)
        {
            try
            {
                string filename = this.dataMonitorModel.ExportSchemeToExcel(pkId);
                string url = string.Empty;
                if (filename != null)
                {
                    url = string.Format("http:\\\\{0}\\Upload\\{1}", Request.Url.Authority, filename);
                }
                return Json(url, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
