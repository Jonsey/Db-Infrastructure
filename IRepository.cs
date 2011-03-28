using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Naimad.Infrastructure.Db
{
    public interface IRepository<T> : IDataSource<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        void Update(T entity);

        void Delete(T entity);

        void DeleteAll();

        void DeleteById(Guid id);

        T GetById(Guid id);

        T GetByIdOrDefault(Guid id);

        IEnumerator<T> GetEnumerator();

    }
}
