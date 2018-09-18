using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.ReportDataModels;

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
                List<ReportDataSourceModel> DataSourceList = db.GetAll<ReportDataSourceModel>(null, false).OrderBy(x => x.Name).ToList();
                ViewBag.DataSourceList = DataSourceList;

                result = db.GetAll<ReportDataSetModel>(new[] { "ReportDataSourceModel" }, false);
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
                List<ReportDataSourceModel> DataSourceList = db.GetAll<ReportDataSourceModel>(null, false).OrderBy(x => x.Name).ToList();
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
    }
}
