using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhaleReport.Models.DBModels.ReportModels
{
    public class ReportDataSetShareModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("ID")]
        public Guid Id { get; set; }
        public Guid ReportDataSetId { get; set; }
        public string ShareUser { get; set; }
    }
}
