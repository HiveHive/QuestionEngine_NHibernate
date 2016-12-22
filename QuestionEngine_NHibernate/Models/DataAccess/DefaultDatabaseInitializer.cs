using System.Configuration;
using NHibernate.Event;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public class DefaultDatabaseInitializer : IDatabaseInitializer
    {
        public string ConnectionString { get; set; }

        public DefaultDatabaseInitializer()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["QuestionEngineDev"].ConnectionString;
        }

        public IPostInsertEventListener GetEntityInsertedListener()
        {
            return new NoOpPostCommitInsertListener();
        }

        public IPostDeleteEventListener GetEntityDeletedListener()
        {
            return new NoOpPostCommitDeleteListener();
        }

    }

    public class NoOpPostCommitDeleteListener : IPostDeleteEventListener
    {
        public void OnPostDelete(PostDeleteEvent @event)
        {
            // do nothing
        }
    }

    public class NoOpPostCommitInsertListener : IPostInsertEventListener
    {
        public void OnPostInsert(PostInsertEvent @event)
        {
            // do nothing
        }
    }
}