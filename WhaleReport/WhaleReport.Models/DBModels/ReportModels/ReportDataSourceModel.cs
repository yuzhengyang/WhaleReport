using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WhaleReport.Models.DBModels.ReportModels
{
    /// <summary>
    /// 报表数据源
    /// </summary>
    public class ReportDataSourceModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("ID")]
        public Guid Id { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        [DisplayName("数据库类型")]
        public string Type { get; set; }
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
        /// 连接字符串
        /// </summary>
        [DisplayName("连接字符串")]
        public string ConnectionString { get; set; }
        public List<ReportDataSetModel> ReportDataSets { get; set; }
        public string CreateUser { get; set; }
        public List<ReportDataSourceShareModel> ReportDataSourceShares { get; set; }
    }
}
