using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WhaleReport.MainDB.DAO;
using WhaleReport.Models.AppModels.ResultModels;
using WhaleReport.Models.DBModels.ReportModels;
using WhaleReport.Modules.DataCovertModule;
using WhaleReport.ReportDB.MySqlUtils;

namespace WhaleReport.Controllers.Apis
{
    public class ReportDataSetApiController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public Rs Share(object paramses)
        {
            //格式化参数
            Dictionary<string, string> p = JsonConvert.DeserializeObject<Dictionary<string, string>>(paramses.ToString());

            Rs rs = Rs.Ok();
            if (p.TryGetValue("__id", out string _id) && Guid.TryParse(_id, out Guid id))
            {
                using (Muse db = new Muse())
                {
                    var record = db.Get<ReportDataSetModel>(x => x.ShareId == id && id != Guid.Empty, new[] { "ReportDataSourceModel" });

                    string cs = record.ReportDataSourceModel.ConnectionString;
                    string sql = record.Sql;

                    MySqlHelper msHelper = new MySqlHelper(cs);
                    DataTable dt = msHelper.Select(sql, out int recordsAffected);
                    var dic = TableConvert.Table2Dictionary(dt);
                    rs.Add("table", dic);
                    rs.Add("recordsAffected", recordsAffected);
                    return rs;
                }
            }
            else
            {
                rs = Rs.Error();
            }
            return rs;
        }
    }
}
