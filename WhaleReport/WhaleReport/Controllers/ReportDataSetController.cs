using Azylee.Core.DataUtils.StringUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.AppModels.ResultModels;
using WhaleReport.Models.DBModels.ReportModels;
using WhaleReport.Modules.DataCovertModule;
using WhaleReport.ReportDB.MySqlUtils;

namespace WhaleReport.Controllers
{
    public class ReportDataSetController : Controller
    {
        // GET: ReportDataSet
        public ActionResult Index()
        {
            IEnumerable<ReportDataSetModel> result = new List<ReportDataSetModel>();
            using (Muse db = new Muse())
            {
                List<ReportDataSourceModel> DataSourceList = db.Gets<ReportDataSourceModel>(x => x.CreateUser == User.Identity.Name, null).OrderBy(x => x.Name).ToList();
                ViewBag.DataSourceList = DataSourceList;

                result = db.Gets<ReportDataSetModel>(x => x.CreateUser == User.Identity.Name, new[] { "ReportDataSourceModel" });
            }
            return View(result);
        }

        // GET: ReportDataSet/Details/5
        public ActionResult Details(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSetModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // GET: ReportDataSet/Create
        public ActionResult Create()
        {
            using (Muse db = new Muse())
            {
                List<ReportDataSourceModel> DataSourceList = db.Gets<ReportDataSourceModel>(x => x.CreateUser == User.Identity.Name, null).OrderBy(x => x.Name).ToList();
                ViewBag.DataSourceList = DataSourceList;
                return View();
            }
        }

        // POST: ReportDataSet/Create
        [HttpPost]
        public ActionResult Create(ReportDataSetModel model)
        {
            using (Muse db = new Muse())
            {
                model.Id = Guid.NewGuid();
                model.CreateUser = User.Identity.Name;

                int flag = db.Add(model, true);
                if (flag > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: ReportDataSet/Edit/5
        public ActionResult Edit(Guid id)
        {
            using (Muse db = new Muse())
            {
                List<ReportDataSourceModel> DataSourceList = db.Gets<ReportDataSourceModel>(x => x.CreateUser == User.Identity.Name, null).OrderBy(x => x.Name).ToList();
                ViewBag.DataSourceList = DataSourceList;
                var record = db.Get<ReportDataSetModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // POST: ReportDataSet/Edit/5
        [HttpPost]
        public ActionResult Edit(ReportDataSetModel model)
        {
            using (Muse db = new Muse())
            {
                int flag = db.Update(model, true);
                if (flag > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: ReportDataSet/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSetModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // POST: ReportDataSet/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, ReportDataSetModel model)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSetModel>(x => x.Id == id, null);
                int flag = db.Del(record, true);
                if (flag > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpPost]
        public JsonResult SetShare(Guid id)
        {
            Rs rs = Rs.Ok("共享成功");
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSetModel>(x => x.Id == id && id != Guid.Empty, null);
                record.ShareId = Guid.NewGuid();
                if (db.Update(record, true) > 0)
                {
                    rs.Add("record", record);
                }
                else
                {
                    rs = Rs.Error("共享失败");
                    rs.Add("record", record);
                }
                return Json(rs, JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public ActionResult Table(Guid id)
        {
            var a = Request.QueryString;
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSetModel>(x => x.ShareId == id && id != Guid.Empty, new[] { "ReportDataSourceModel" });

                string cs = record.ReportDataSourceModel.ConnectionString;
                string sql = record.Sql;

                MySqlHelper msHelper = new MySqlHelper(cs);
                DataTable dt = msHelper.Select(sql, out int recordsAffected);
                var dic = TableConvert.Table2Dictionary(dt);
                return View(dic);
            }
        }
    }
}
