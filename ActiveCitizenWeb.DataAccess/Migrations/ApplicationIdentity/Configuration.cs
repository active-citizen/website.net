using System.Data.Entity.Migrations;

namespace ActiveCitizenWeb.DataAccess.Migrations.ApplicationIdentity
{

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationIdentity";
        }
    }
}
