using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.ReportDataModels
{
    public class ReportModel
    {
        public DataTable DataTable { get; set; }
        public string DataTableJson()
        {
            if (DataTable != null)
                return JsonConvert.SerializeObject(DataTable);
            return "";
        }
        public string[] Columns()
        {
            List<string> dim = new List<string>();
            if (DataTable != null && DataTable.Columns != null)
            {
                foreach (var item in DataTable.Columns)
                {
                    dim.Add(item.ToString());
                }
            }
            return dim.ToArray();
        }
        public ReportOptionModel ReportOptionModel { get; set; }
    }
}
