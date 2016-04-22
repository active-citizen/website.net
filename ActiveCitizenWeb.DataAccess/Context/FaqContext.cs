namespace ActiveCitizenWeb.DataAccess.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ActiveCitizen.Model.StaticContent.FAQ;

    public class FaqContext : DbContext, IFaqContext
    {
        public FaqContext()
            : base("name=FaqContext")
        {
            Database.SetInitializer<FaqContext>(null);
        }

        public IDbSet<FaqListItem> FaqListItem { get; set; }
        public IDbSet<FaqListCategory> FaqListCategory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FaqListItem>().ToTable("FaqListItems");
            modelBuilder.Entity<FaqListCategory>().ToTable("FaqListCategories");
        }
    }
}