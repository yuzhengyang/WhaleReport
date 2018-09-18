using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.ReportDataModels
{
    public class ReportPageModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }
        [DisplayName("报表名称")]
        public string Name { get; set; }
        [DisplayName("报表描述")]
        public string Description { get; set; }
        public List<ReportOptionModel> ReportOptionModels { get; set; }
    }
}
