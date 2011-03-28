using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naimad.Infrastructure.Db
{
    public interface IDataSource<T> : IQueryable<T> where T : IAggregateRoot
    {
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(Guid id);
        T Add(T entity);     
    }
}
