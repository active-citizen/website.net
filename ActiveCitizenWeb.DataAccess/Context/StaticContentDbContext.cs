using System.Data.Entity;
using ActiveCitizen.Model.StaticContent.Faq;

namespace ActiveCitizenWeb.DataAccess.Context
{
    public class StaticContentDbContext : DbContext, IStaticContentDbContext
    {
        private readonly IRepository<int, FaqListCategory> faqListCategory;
        private readonly IRepository<int, FaqListItem> faqListItem;

        static StaticContentDbContext()
        {
            // Workaround for SqlClient provider missing in the site bin folder after build
            var ensureSqlClientProvider = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public StaticContentDbContext()
            : base("ActiveCitizen")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StaticContentDbContext, Migrations.StaticContent.Configuration>(true));

            faqListCategory = new RepositoryBase<int, FaqListCategory>(this, cat => cat.Id);

            faqListItem = new RepositoryBase<int, FaqListItem>(this, item => item.Id);
        }

        public IDbSet<FaqListItem> FaqListItem { get; set; }

        public IDbSet<FaqListCategory> FaqListCategory { get; set; }

        IRepository<int, FaqListCategory> IStaticContentDbContext.FaqListCategory
        {
            get { return faqListCategory; }
        }

        IRepository<int, FaqListItem> IStaticContentDbContext.FaqListItem
        {
            get { return faqListItem; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FaqListItem>().ToTable("FaqListItems");
            modelBuilder.Entity<FaqListCategory>().ToTable("FaqListCategories");
        }
    }
}