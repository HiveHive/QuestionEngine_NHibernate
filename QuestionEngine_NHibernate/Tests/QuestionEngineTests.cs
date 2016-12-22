using System;
using NUnit.Framework;
using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate.Tests
{
    [TestFixture]
    public class QuestionEngineTests
    {
        internal static readonly TestCleanup TestCleanUp = new TestCleanup();
        
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            TestCleanUp.CleanUp();
        }
    }

    [SetUpFixture]
    public class TestSetUpFixture
    {
        [SetUp]
        public void SetupAssembly()
        {
            var dbInitializer = new TestDatabaseInitializer(QuestionEngineTests.TestCleanUp);
            DatabaseManager.Instance.Initialize(dbInitializer);
        }
    }
}