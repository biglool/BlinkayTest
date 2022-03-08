using Blinkay.Domain;
using Blinkay.Persistance.Respositories;
using Blinkay.UseCases.Infrastructure;

namespace Blinkay.UseCases.UseCases
{
    public class SelectPlusUpdatePlusInsertionUseCase : ThreadManagedDBUseCase
    {
  
            public SelectPlusUpdatePlusInsertionUseCase(int iNumRegistries, int iNumThreads, string connectionname) : base(iNumRegistries,  iNumThreads, connectionname)
        {
        }

        protected override void BaseTask()
        {
            for (int i = 0; i < _iNumRegistries; i++)
            {
                GenericRepository repo = new GenericRepository(_connectionName);

                Generic genericEntity = new Generic();
                genericEntity.SetRandomText();
                repo.Add(genericEntity);
                repo.UpdateRandom();

            }
        }



    }
}
