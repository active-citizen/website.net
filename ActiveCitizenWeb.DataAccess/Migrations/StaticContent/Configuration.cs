using System.Data.Entity.Migrations;

namespace ActiveCitizenWeb.DataAccess.Migrations.StaticContent
{

    internal sealed class Configuration : DbMigrationsConfiguration<Context.StaticContentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\StaticContent";
        }
    }
}
