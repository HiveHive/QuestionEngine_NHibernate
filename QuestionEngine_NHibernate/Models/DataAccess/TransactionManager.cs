using System;
using System.Threading;

namespace QuestionEngine_NHibernate.Models.DataAccess
{
    public class TransactionManager
    {
        public static dynamic Execute(Func<object> action)
        {
            dynamic result = null;

            try
            {
                using (var unitOfWork = DatabaseManager.Instance.CreateNewUnitOfWork())
                {
                    using (unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            SetUnitOfWorkOnCurrentThread(unitOfWork);
                            result = action.Invoke();
                            unitOfWork.Commit();
                        }
                        catch (Exception e)
                        {
                            unitOfWork.Rollback();
                            throw;
                        }
                    }
                }
            }
            finally
            {
                ClearCurrentUnitOfWorkOnThread();
            }

            return result;
        }


        /*
        * NOTE: 
        * The following code allocates the db context for the started transaction to a memory slot. 
        * This way, the Repository will have access to the current db context.
        */

        private static readonly Object _setUnitOfWorkLock = new Object();

        private static void SetUnitOfWorkOnCurrentThread(IUnitOfWork unitOfWork)
        {
            lock (_setUnitOfWorkLock)
            {
                var localDataStore = Thread.AllocateNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
                Thread.SetData(localDataStore, unitOfWork);
            }
        }

        private static readonly Object _clearUnitOfWorkLock = new Object();

        private static void ClearCurrentUnitOfWorkOnThread()
        {
            lock (_clearUnitOfWorkLock)
            {
                Thread.FreeNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        private static readonly Object _getUnitOfWorkLock = new Object();
        public static IUnitOfWork CurrentUnitOfWork()
        {
            lock (_getUnitOfWorkLock)
            {
                var localDataStore = Thread.GetNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
                var unitOfWork = (IUnitOfWork)Thread.GetData(localDataStore);
                return unitOfWork;
            }
        }
    }
}