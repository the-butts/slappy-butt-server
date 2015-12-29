namespace SlappyButt.Api
{
    using System.Data.Entity;
    using SlappyButt.Data;
    using SlappyButt.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SlappyButtDbContext, Configuration>());
            SlappyButtDbContext.Create().Database.Initialize(true);
        }
    }
}