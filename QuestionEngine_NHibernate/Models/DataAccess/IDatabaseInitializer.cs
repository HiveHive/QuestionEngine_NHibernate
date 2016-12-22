using NHibernate.Event;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public interface IDatabaseInitializer
    {
        string ConnectionString { get; set; }

        IPostInsertEventListener GetEntityInsertedListener();
        IPostDeleteEventListener GetEntityDeletedListener();
    }

    public interface IEntityDeletedListener
    {
        void NotifyDeleted(IEntity entity);
    }

    public interface IEntityInsertedListener
    {
        void NotifyCreated(IEntity entity);
    }
}