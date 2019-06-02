using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WhaleReport.Models.AppModels.ReportModels;

namespace WhaleReport.Models.DBModels.ReportModels
{
    /// <summary>
    /// 报表数据集
    /// </summary>
    public class ReportDataSetModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("ID")]
        public Guid Id { get; set; }
        [DisplayName("数据源")]
        public Guid ReportDataSourceModelId { get; set; }
        public ReportDataSourceModel ReportDataSourceModel { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }
        /// <summary>
        /// 查询语句
        /// </summary>
        [DisplayName("查询语句")]
        public string Sql { get; set; }
        public List<ReportOptionModel> ReportOptionModels { get; set; }
        public string  CreateUser { get; set; }
        public Guid ShareId { get; set; }
    }
}
