using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Naimad.Infrastructure.Db
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private IDataSource<T> dataSource;

        protected Repository() { }

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            dataSource = unitOfWork.GetDataSource<T>();
            UnitOfWork = unitOfWork;
        }

        public T Add(T entity)
        {
            return dataSource.Add(entity);
        }  

        public void Update(T entity)
        {
            dataSource.Update(entity);
        }

        public void Delete(T entity)
        {
            dataSource.Delete(entity);
        }

        public void DeleteAll()
        {
            foreach (var e in this)
                dataSource.Delete(e);
        }

        public void DeleteById(Guid id)
        {
            dataSource.DeleteById(id);
        }

        public T GetById(Guid id)
        {
            return dataSource.Single(x => x.Id == id);
        }

        public T GetByIdOrDefault(Guid id)
        {
            return dataSource.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dataSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return dataSource.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return dataSource.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return dataSource.Provider;
            }
        }

        public IUnitOfWork UnitOfWork { get; private set; }
    }
}
