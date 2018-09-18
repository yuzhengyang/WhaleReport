using System.Data.Entity.Migrations;
using WhaleReport.MainDB.DAO;

namespace WhaleReport.MainDB.Migrations
{
    internal sealed class MigraConf : DbMigrationsConfiguration<DbTable>
    {
        public MigraConf()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = "ContextKey";
        }

        protected override void Seed(DbTable context)
        {
        }
    }
}
