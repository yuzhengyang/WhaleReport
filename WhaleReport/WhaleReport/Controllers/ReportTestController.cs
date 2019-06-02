using Azylee.Core.DataUtils.CollectionUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.AppModels.ReportModels;
using WhaleReport.Models.DBModels.ReportModels;
using WhaleReport.ReportDB.MySqlUtils;

namespace WhaleReport.Controllers
{
    public class ReportTestController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            using (Muse db = new Muse())
            {
                IEnumerable<ReportDataSourceModel> sources = db.GetAll<ReportDataSourceModel>(new string[] { "ReportDataSetModels" }, false);

                if (Ls.Ok(sources))
                {
                    var source = sources.First();
                    string cs = source.ConnectionString;
                    string sql = source.ReportDataSetModels.First().Sql;

                    MySqlHelper msHelper = new MySqlHelper(cs);
                    DataTable a = msHelper.Select(sql, out int recordsAffected);
                    ReportModel ri = new ReportModel()
                    {
                        DataTable = a,
                        ReportOptionModel = new ReportOptionModel()
                        {
                            Title = "标题XXXXXX",
                            Type = "ECharts.BasicLineChart",
                            Dimensions = "continent,cts,rgs",
                            SeriesType = "bar,bar",
                            Height = 400,
                        }
                    };
                    ViewBag.Report = ri;
                }
            }
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(Dictionary<string, object> values)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
