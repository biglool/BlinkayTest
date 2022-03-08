using System.Threading;

namespace Blinkay.UseCases.Infrastructure
{

   public abstract class ThreadManagedDBUseCase : ThreadManager
    {
        protected int _iNumRegistries;
        protected string _connectionName ;

        protected ThreadManagedDBUseCase(int iNumRegistries, int iNumThreads, string connectionName) : base(iNumThreads)
        {
            _iNumRegistries = iNumRegistries;
            _connectionName = connectionName;

        }

        public int Execute()
        {
            return base.executeThreads(new ThreadStart(BaseTask));
        }

       protected abstract void BaseTask();


    }
}
