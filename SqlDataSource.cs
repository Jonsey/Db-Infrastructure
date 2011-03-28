using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace Naimad.Infrastructure.Db
{
    public class SqlDataSource<T> : IDataSource<T> where T : IAggregateRoot
    {
        private ISession session;

        public SqlDataSource(ISession context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            session = context;
        }

        #region IDataSource<T> Members

        public void Update(T entity)
        {
            session.Update(entity);
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
        }

        public void DeleteById(Guid id)
        {
            var e = (from x in session.Linq<T>() where x.Id == id select x).Single();
            session.Delete(e);
        }

        public T Add(T entity)
        {
            session.SaveOrUpdate(entity);
            return entity;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return (from x in session.Linq<T>() select x).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return (from x in session.Linq<T>() select x).Expression; }
        }

        public IQueryProvider Provider
        {
            get { return (from x in session.Linq<T>() select x).Provider; }
        }

        #endregion
    }
}
