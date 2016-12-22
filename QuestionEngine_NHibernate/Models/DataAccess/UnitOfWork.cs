using System;
using NHibernate;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private ISession Session { get; set; }

        public UnitOfWork(ISession session)
        {
            Session = session;
        }

        public void Dispose()
        {
            Session.Dispose();
        }

        public ITransaction BeginTransaction()
        {
            return Session.BeginTransaction();
        }

        public void Commit()
        {
            Session.Transaction.Commit();
        }

        public void Rollback()
        {
            Session.Transaction.Rollback();
        }

        public IQueryOver<T,T> Query<T>() where T : class
        {
            return Session.QueryOver<T>();
        }

        public void Delete(IEntity entity)
        {
            Session.Delete(entity);
        }

        public IEntity Find(Type type, int id)
        {
            return Session.Get(type, id) as IEntity;
        }

        public void AddEntity(IEntity entity)
        {
            Session.SaveOrUpdate(entity);
        }
    }
}