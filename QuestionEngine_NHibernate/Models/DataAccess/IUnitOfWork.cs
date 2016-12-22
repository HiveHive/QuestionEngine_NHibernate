using System;
using NHibernate;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ITransaction BeginTransaction();
        void Commit();
        void Rollback();

        void AddEntity(IEntity entity);
        void Delete(IEntity entity);

        IQueryOver<T,T> Query<T>() where T: class;
        IEntity Find(Type type, int id);
    }
}