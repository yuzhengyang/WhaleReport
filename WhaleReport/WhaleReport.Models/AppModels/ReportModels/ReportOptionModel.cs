using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleReport.Models.DBModels.ReportModels;

namespace WhaleReport.Models.AppModels.ReportModels
{
    public class ReportOptionModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }
        /// <summary>
        /// 图表标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }
        /// <summary>
        /// 图表类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }
        /// <summary>
        /// 图表数据列顺序
        /// </summary>
        [DisplayName("数据列")]
        public string Dimensions { get; set; }
        public string[] DimensionsList { get { return Dimensions?.Split(','); } }
        /// <summary>
        /// 图表数据列类型
        /// </summary>
        [DisplayName("列类型")]
        public string SeriesType { get; set; }
        public string[] SeriesTypeList { get { return SeriesType?.Split(','); } }
        [DisplayName("名称列")]
        public string DataName { get; set; }
        [DisplayName("分组列")]
        public string DataGroup { get; set; }
        [DisplayName("数据列")]
        public string DataValue { get; set; }
        [DisplayName("数据集合")]
        public Guid ReportDataSetModelId { get; set; }
        public ReportDataSetModel ReportDataSetModel { get; set; }
        public Guid ReportPageModelId { get; set; }
        public ReportPageModel ReportPageModel { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        [DisplayName("行")]
        public int Row { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        [DisplayName("列")]
        public int Column { get; set; }
        /// <summary>
        /// 列宽样式定义
        /// </summary>
        [DisplayName("列样式")]
        public string ColumnClass { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [DisplayName("高度")]
        public int Height { get; set; }
        ///// <summary>
        ///// 宽度
        ///// </summary>
        //[DisplayName("宽度")]
        //public int Width { get; set; }
    }
}
