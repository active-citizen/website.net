using System.Data.Entity;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Data.Entity.Infrastructure;
using System;

namespace ActiveCitizenWeb.DataAccess.Context
{
    public interface IFaqContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();

        IDbSet<FaqListCategory> FaqListCategory { get; set; }
        IDbSet<FaqListItem> FaqListItem { get; set; }
    }
}