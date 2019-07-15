using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.DBModels.ReportModels
{
    public class ReportDataSourceShareModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("ID")]
        public Guid Id { get; set; }
        public Guid ReportDataSourceId { get; set; }
        public string ShareUser { get; set; }
    }
}
