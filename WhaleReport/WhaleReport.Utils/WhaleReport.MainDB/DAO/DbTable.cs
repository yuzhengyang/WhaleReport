using System.Data.Entity;
using WhaleReport.Models.DBModels.ReportModels;
using WhaleReport.Models.DBModels.UserModels;

namespace WhaleReport.MainDB.DAO
{
    internal class DbTable : SuperDb
    {
        public DbTable() : base()
        {
        }

        #region 数据库表
        public DbSet<ReportDataSetModel> ReportDataSet { get; set; }
        public DbSet<ReportDataSetShareModel> ReportDataSetShare { get; set; }
        public DbSet<ReportDataSourceModel> ReportDataSource { get; set; }
        public DbSet<ReportDataSourceShareModel> ReportDataSourceShare { get; set; }
        public DbSet<ReportPageModel> ReportPage { get; set; }
        public DbSet<ReportPageShareModel> ReportPageShare { get; set; }
        public DbSet<ReportOptionModel> ReportOption { get; set; }
        public DbSet<AuthorizeModel> Authorize { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //一对一关系
            //modelBuilder.Entity<Class1>().HasRequired(p => p.Users).WithOptional(p => p.Class1);
        }
    }
}
