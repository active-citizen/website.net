using ActiveCitizenWeb.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Data.Entity;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ActiveCitizenWeb.Tests.ContentProviders
{
    internal class FaqListItemDbSetMock : IDbSet<FaqListItem>
    {
        private readonly IQueryable<FaqListItem> items;

        public FaqListItemDbSetMock(List<FaqListItem> items)
        {
            this.items = items.AsQueryable();
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

        public ObservableCollection<FaqListItem> Local
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

        public FaqListItem Add(FaqListItem entity)
        {
            throw new NotImplementedException();
        }

        public FaqListItem Attach(FaqListItem entity)
        {
            throw new NotImplementedException();
        }

        public FaqListItem Create()
        {
            throw new NotImplementedException();
        }

        public FaqListItem Find(params object[] keyValues)
        {
            return items.FirstOrDefault(i=>i.Id.Equals(keyValues[0]));
        }

        public IEnumerator<FaqListItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public FaqListItem Remove(FaqListItem entity)
        {
            throw new NotImplementedException();
        }

        TDerivedEntity IDbSet<FaqListItem>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }

    internal class FaqDbContextMock : IFaqContext
    {
        private readonly IDbSet<FaqListItem> items;

        public FaqDbContextMock(List<FaqListItem> items)
        {
            this.items = new FaqListItemDbSetMock(items);
        }

        public IDbSet<FaqListCategory> FaqListCategory
        {
            get
            {
                throw new NotImplementedException();
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
    }
}
