using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;

namespace Naimad.Infrastructure.Db
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private ISession session;

        public SqlUnitOfWork(ISession session)
        {
            session.FlushMode = FlushMode.Commit;
            this.session = session;
        }

        public void Dispose()
        {
            Close();
        }

        public IDataSource<T> GetDataSource<T>() where T : IAggregateRoot
        {
            return new SqlDataSource<T>(session);
        }

        public void SetFlushModeAuto()
        {
            session.FlushMode = FlushMode.Auto;
        }

        public void SetFlushModeCommit()
        {
            session.FlushMode = FlushMode.Commit;
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (session != null && !session.Transaction.IsActive)
                session.Transaction.Begin(isolationLevel);
        }

        public void RollbackTransaction()
        {
            if (session.Transaction != null && session.Transaction.IsActive)
                session.Transaction.Rollback();
        }

        public void TransactionalFlush()
        {
            TransactionalFlush(IsolationLevel.ReadUncommitted);
        }

        public void TransactionalFlush(IsolationLevel isolationLevel)
        {
            TransactionalFlush(isolationLevel, false);
        }

        public void TransactionalFlush(IsolationLevel isolationLevel, bool batchMode)
        {
            BeginTransaction(isolationLevel);
            CommitTransaction(batchMode);
        }

        public void CommitTransaction()
        {
            CommitTransaction(false);
        }

        public void CommitTransaction(bool batchMode)
        {
            try
            {
                if (batchMode)
                    session.SetBatchSize(50);

                session.Flush();

                if (batchMode)
                    session.SetBatchSize(0);

                if (session.Transaction != null && session.Transaction.IsActive)
                    session.Transaction.Commit();
            }
            catch (Exception ex)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                    session.Transaction.Rollback();

                session.Clear();
                throw ex;
            }
        }

        public void Detach<T>(T entity)
        {
            session.Evict(entity);
        }

        public void DoNotSaveAnyMoreChanges()
        {
            session.FlushMode = FlushMode.Never;
        }

        /// <summary>
        /// Completely clear the session and cancel all pending saves, updates and deletions.
        /// </summary>
        public void ClearCache()
        {
            session.Clear();
        }

        /// <summary>
        /// Will eager load the entity(s)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Initialise<T>(T entity)
        {
            NHibernateUtil.Initialize(entity);
        }


        public void StopTransaction()
        {
            if (session.Transaction != null && session.Transaction.IsActive)
                session.Transaction.Commit();
        }

        public bool IsOpen
        {
            get { return session.IsOpen; }
        }

        public void Close()
        {
            if (session != null)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                    session.Transaction.Rollback();
                session.Close();
                session.Dispose();
                session = null;
            }
        }

        public ISession GetUnderlyingSession()
        {
            return session;
        }
    }
}
