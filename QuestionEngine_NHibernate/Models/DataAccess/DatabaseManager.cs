using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using QuestionEngine_NHibernate.Models.DataAccess.Mappings;
using QuestionEngine_NHibernate.Models.Domain.Questions;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;

        public static DatabaseManager Instance
        {
            get { return _instance ?? (_instance = new DatabaseManager()); }
        }

        private ISessionFactory SessionFactory { get; set; }

        private DatabaseManager()
        {
        }

        public void Initialize(IDatabaseInitializer dbInitializer)
        {
            var config = BuildConfiguration(dbInitializer);
            SetEventListeners(config, dbInitializer);
            //BuildSchema(config);
            RunMigrations(dbInitializer);
            SessionFactory = config.BuildSessionFactory();
        }

        private void RunMigrations(IDatabaseInitializer dbInitializer)
        {
             var migrator = new Migrator(dbInitializer.ConnectionString);
             migrator.Migrate(runner => runner.MigrateUp());
        }

        private void SetEventListeners(Configuration config, IDatabaseInitializer dbInitializer)
        {
            config.SetListener(ListenerType.PostCommitInsert, dbInitializer.GetEntityInsertedListener());
            config.SetListener(ListenerType.PostCommitDelete, dbInitializer.GetEntityDeletedListener());
        }

        private Configuration BuildConfiguration(IDatabaseInitializer dbInitializer)
        {
            var config = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(dbInitializer.ConnectionString))
                .Mappings(DbMappings.Mappings)
                .BuildConfiguration();

            return config;
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }

        public IUnitOfWork CreateNewUnitOfWork()
        {
            return new UnitOfWork(SessionFactory.OpenSession());
        }
    }

    public class Migrator
    {
        private string ConnectionString { get; set; }

        public Migrator(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; private set; }
        }

        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            // var announcer = new NullAnnouncer();
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = "QuestionEngine_NHibernate.Migrations"
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.MySql.MySqlProcessorFactory();

            using (var processor = factory.Create(ConnectionString, announcer, options))
            {
                var runner = new MigrationRunner(assembly, migrationContext, processor);
                runner.MigrateUp(true);
            }
        }
    }
}