using Azylee.Core.DataUtils.CollectionUtils;
using Azylee.Core.DataUtils.StringUtils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.AppModels.ReportModels;
using WhaleReport.Models.AppModels.ResultModels;
using WhaleReport.Models.DBModels.ReportModels;
using WhaleReport.ReportDB.MySqlUtils;

namespace WhaleReport.Controllers
{
    public class ReportPageController : Controller
    {
        List<ReportOptionTypeModel> ReportOptionTypeList = new List<ReportOptionTypeModel>()
        {
           new ReportOptionTypeModel() {Name="柱状图",Type="ECharts.BarSimple"},
           new ReportOptionTypeModel() {Name="折线图",Type="ECharts.BasicLineChart"},
           new ReportOptionTypeModel() {Name="散点图",Type="ECharts.BasicScatterChart"},
        };
        // GET: ReportPage
        public ActionResult Index()
        {
            IEnumerable<ReportPageModel> result = new List<ReportPageModel>();
            using (Muse db = new Muse())
            {
                result = db.Gets<ReportPageModel>(x => x.CreateUser == User.Identity.Name, null).OrderBy(x => x.Name);
            }
            return View(result);
        }

        // GET: ReportPage/Details/5
        public ActionResult Details(Guid id)
        {
            using (Muse db = new Muse())
            {
                ReportPageModel record = db.Get<ReportPageModel>(x => x.Id == id,
                    new[] { "ReportOptionModels",
                        "ReportOptionModels.ReportDataSetModel",
                        "ReportOptionModels.ReportDataSetModel.ReportDataSourceModel" });
                record.ReportOptionModels = record.ReportOptionModels.OrderBy(x => x.Row).ThenBy(x => x.Column).ToList();

                List<ReportDataSourceModel> DataSourceList = db.Gets<ReportDataSourceModel>(x => x.CreateUser == User.Identity.Name, new[] { "ReportDataSetModels" }).ToList();
                ViewBag.DataSourceList = DataSourceList;
                ViewBag.ReportOptionTypeList = ReportOptionTypeList;
                return View(record);
            }
        }

        public ActionResult Preview(Guid id)
        {
            NameValueCollection values = Request.QueryString;

            List<ReportModel> ReportModels = new List<ReportModel>();

            using (Muse db = new Muse())
            {
                var record = db.Get<ReportPageModel>(x => x.Id == id,
                    new[] { "ReportOptionModels",
                        "ReportOptionModels.ReportDataSetModel",
                        "ReportOptionModels.ReportDataSetModel.ReportDataSourceModel" });

                if (record != null && Ls.Ok(record.ReportOptionModels))
                {
                    foreach (var option in record.ReportOptionModels)
                    {
                        string cs = option.ReportDataSetModel.ReportDataSourceModel.ConnectionString;
                        string sql = option.ReportDataSetModel.Sql;

                        MySqlHelper msHelper = new MySqlHelper(cs);
                        DataTable dt = msHelper.Select(sql, out int recordsAffected);
                        if (Str.Ok(option.DataName, option.DataGroup, option.DataValue))
                        {
                            dt = msHelper.Group(dt, option.DataName, option.DataValue, option.DataGroup);
                        }

                        ReportModels.Add(new ReportModel()
                        {
                            DataTable = dt,
                            ReportOptionModel = option,
                        });
                    }
                    ReportModels = ReportModels.OrderBy(x => x.ReportOptionModel.Row).ThenBy(x => x.ReportOptionModel.Column).ToList();
                    ViewBag.ReportModels = ReportModels;
                }

                return View(record);
            }
        }

        // POST: ReportPage/Create
        [HttpPost]
        public ActionResult Create(ReportPageModel model)
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

        // POST: ReportPage/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ReportPageModel model)
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

        // POST: ReportPage/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, ReportPageModel model)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportPageModel>(x => x.Id == id, null);
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
        public ActionResult AddOption(ReportOptionModel model)
        {
            using (Muse db = new Muse())
            {
                model.Id = Guid.NewGuid();

                int flag = db.Add(model, true);
                if (flag > 0)
                {
                    return RedirectToAction("Details", new { id = model.ReportPageModelId });
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult EditOption(ReportOptionModel model)
        {
            using (Muse db = new Muse())
            {
                int flag = db.Update(model, true);
                if (flag > 0)
                {
                    return RedirectToAction("Details", new { id = model.ReportPageModelId });
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteOption(Guid id)
        {
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportOptionModel>(x => x.Id == id, null);
                int flag = db.Del(record, true);
                if (flag > 0)
                {
                    return RedirectToAction("Details", new { id = record.ReportPageModelId });
                }
                else
                {
                    return View();
                }
            }
        }

        public JsonResult SetShare(Guid id)
        {
            Rs rs = Rs.Ok("共享成功");
            using (Muse db = new Muse())
            {
                var record = db.Get<ReportPageModel>(x => x.Id == id && id != Guid.Empty, null);
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
        public ActionResult Share(Guid id)
        {
            using (Muse db = new Muse())
            {
                //验证共享ID是否正确
                var record = db.Get<ReportPageModel>(x => x.ShareId == id && id != Guid.Empty,
                    new[] { "ReportOptionModels",
                        "ReportOptionModels.ReportDataSetModel",
                        "ReportOptionModels.ReportDataSetModel.ReportDataSourceModel" });

                if (record != null && Ls.Ok(record.ReportOptionModels))
                {
                    List<ReportModel> ReportModels = new List<ReportModel>();
                    foreach (var option in record.ReportOptionModels)
                    {
                        string cs = option.ReportDataSetModel.ReportDataSourceModel.ConnectionString;
                        string sql = option.ReportDataSetModel.Sql;

                        MySqlHelper msHelper = new MySqlHelper(cs);
                        DataTable dt = msHelper.Select(sql, out int recordsAffected);
                        if (Str.Ok(option.DataName, option.DataGroup, option.DataValue))
                        {
                            dt = msHelper.Group(dt, option.DataName, option.DataValue, option.DataGroup);
                        }

                        ReportModels.Add(new ReportModel()
                        {
                            DataTable = dt,
                            ReportOptionModel = option,
                        });
                    }
                    ReportModels = ReportModels.OrderBy(x => x.ReportOptionModel.Row).ThenBy(x => x.ReportOptionModel.Column).ToList();
                    ViewBag.ReportModels = ReportModels;
                }

                return View(record);
            }
        }
    }
}
