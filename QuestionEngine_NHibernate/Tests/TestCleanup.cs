using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Event;
using QuestionEngine_NHibernate.Models.DataAccess;

namespace QuestionEngine_NHibernate.Tests
{
    public class TestCleanup : IPostInsertEventListener, IPostDeleteEventListener
    {
        private readonly List<Tuple<Type, int>> _entityTypeIdPairs = new List<Tuple<Type, int>>();

        public void CleanUp()
        {
            _entityTypeIdPairs.Reverse();

            foreach (var entityTypeIdPair in _entityTypeIdPairs.ToList())
            {
                TransactionManager.Execute(() =>
                {
                    var entity = TransactionManager.CurrentUnitOfWork().Find(entityTypeIdPair.Item1, entityTypeIdPair.Item2);
                    if (entity != null)
                    {
                        TransactionManager.CurrentUnitOfWork().Delete(entity);
                    }
                    return null;
                });
            }
           
            _entityTypeIdPairs.Clear();
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            var entity = @event.Entity as IEntity;
            if (entity != null) 
                _entityTypeIdPairs.Add(new Tuple<Type, int>(entity.GetType(), entity.Id));
        }

        public void OnPostDelete(PostDeleteEvent @event)
        {
            var entity = @event.Entity as IEntity;
            if (entity != null)
                _entityTypeIdPairs.RemoveAll(x => x.Item1 == entity.GetType() && x.Item2 == entity.Id);
        }
    }
}