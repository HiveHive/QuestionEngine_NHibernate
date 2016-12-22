using System.Configuration;
using NHibernate.Event;
using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate.Tests
{
    public class TestDatabaseInitializer : IDatabaseInitializer
    {
        public TestCleanup TestCleanup { get; set; }
        public string ConnectionString { get; set; }
        
        public TestDatabaseInitializer(TestCleanup testCleanup)
        {
            TestCleanup = testCleanup;
            ConnectionString = ConfigurationManager.ConnectionStrings["QuestionEngineTest"].ConnectionString;
        }

        public IPostInsertEventListener GetEntityInsertedListener()
        {
            return TestCleanup;
        }

        public IPostDeleteEventListener GetEntityDeletedListener()
        {
            return TestCleanup;
        }

    }
}