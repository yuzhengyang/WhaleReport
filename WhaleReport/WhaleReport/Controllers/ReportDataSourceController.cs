using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.ReportDataModels;

namespace WhaleReport.Controllers
{
    public class ReportDataSourceController : Controller
    {
        // GET: ReportDataSource
        public ActionResult Index()
        {
            IEnumerable<ReportDataSourceModel> result = new List<ReportDataSourceModel>();
            using (Muse db = new Muse())
            {
                result = db.GetAll<ReportDataSourceModel>(null, false);
            }
            return View(result);
        }

        // GET: ReportDataSource/Details/5
        public ActionResult Details(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSourceModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // GET: ReportDataSource/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportDataSource/Create
        [HttpPost]
        public ActionResult Create(ReportDataSourceModel model)
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

            //try
            //{
            //    // TODO: Add insert logic here
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: ReportDataSource/Edit/5
        public ActionResult Edit(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSourceModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // POST: ReportDataSource/Edit/5
        [HttpPost]
        public ActionResult Edit(ReportDataSourceModel model)
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
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: ReportDataSource/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSourceModel>(x => x.Id == id, null);
                return View(record);
            }
        }

        // POST: ReportDataSource/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, ReportDataSourceModel model)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportDataSourceModel>(x => x.Id == id, null);
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
