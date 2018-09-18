﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WhaleReport.MainDB.Migrations;

namespace WhaleReport.MainDB.DAO
{
    /// <summary>
    /// SuperDb 中完成数据库的配置
    /// </summary>
    internal class SuperDb : DbContext
    {
        public SuperDb()
            : base("DefaultConnection")//SQLServerConnection//DefaultConnection
        {
            //设置数据迁移配置
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbTable, MigraConf>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //去掉复数映射
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
