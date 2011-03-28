using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Naimad.Infrastructure.Db
{
    public interface IUnitOfWork
    {
        IDataSource<T> GetDataSource<T>() where T : IAggregateRoot;

        void SetFlushModeAuto();

        void SetFlushModeCommit();

        void BeginTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);
        void RollbackTransaction();

        void CommitTransaction();
        void CommitTransaction(bool batchMode);

        /// <summary>
        /// Removes the entity from the session so that no changes will be saved from this point on
        /// </summary>
        /// <typeparam name="T">Type of entity to detach</typeparam>
        /// <param name="entity">Entity being detached</param>
        void Detach<T>(T entity);

        /// <summary>
        /// Will eager load the entity(s)
        /// </summary>
        void Initialise<T>(T entity);

        void Close();

        /// <summary>
        /// Stops the session from saving any more changes. ie turns flush mode off
        /// </summary>
        void DoNotSaveAnyMoreChanges();

        /// <summary>
        /// Dumps all objects in the session so forcing requerying for any entities from there on
        /// </summary>
        void ClearCache();

        bool IsOpen { get; }

        void TransactionalFlush();
        void TransactionalFlush(IsolationLevel isolationLevel);
        void TransactionalFlush(IsolationLevel isolationLevel, bool batchMode);
    }
}
