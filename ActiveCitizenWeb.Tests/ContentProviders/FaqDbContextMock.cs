using ActiveCitizenWeb.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using ActiveCitizen.Model.StaticContent.Faq;
using System.Data.Entity;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace ActiveCitizenWeb.Tests.ContentProviders
{
    internal class DbSetMock<T> : IDbSet<T> where T : class
    {
        private readonly IQueryable<T> items;
        private readonly Func<T, object[], bool> findByKeysExpression;

        public DbSetMock(List<T> items, Func<T, object[], bool> findByKeysExpression)
        {
            this.items = items.AsQueryable();
            this.findByKeysExpression = findByKeysExpression;
        }

        public Type ElementType
        {
            get
            {
                return items.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return items.Expression;
            }
        }

        public ObservableCollection<T> Local
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return items.Provider;
            }
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            return items.FirstOrDefault(i => findByKeysExpression(i, keyValues));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public T Remove(T entity)
        {
            throw new NotImplementedException();
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
    /*
    internal class FaqDbContextMock : IFaqContext
    {
        DbEntityEntry<string>  e = new DbEntityEntry<string>("");

        private readonly IDbSet<FaqListItem> items;

        private readonly IDbSet<FaqListCategory> categories;

        public FaqDbContextMock(List<FaqListItem> items, List<FaqListCategory> categories)
        {
            if (items != null)
            {
                this.items = new DbSetMock<FaqListItem>(items, (entry, keys) => entry.Id.Equals(keys[0]));
            }
            if (categories != null)
            {
                this.categories = new DbSetMock<FaqListCategory>(categories, (entry, keys) => entry.Id.Equals(keys[0]));
            }
        }

        public IDbSet<FaqListCategory> FaqListCategory
        {
            get
            {
                return categories;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<FaqListItem> FaqListItem
        {
            get
            {
                return items;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
    */
}
