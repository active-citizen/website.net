using System.Data.Entity;
using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizenWeb.DataAccess.Context
{
    public class ApplicationIdentityDbContext : IdentityDbContext<LdapIdentityUser>
    {
        public ApplicationIdentityDbContext() : base("ActiveCitizen.Auth", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationIdentityDbContext, Migrations.ApplicationIdentity.Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("auth");
        }
    }
}