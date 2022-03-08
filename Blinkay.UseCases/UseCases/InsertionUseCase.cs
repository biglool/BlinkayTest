using Blinkay.Domain;
using Blinkay.Persistance.Respositories;
using Blinkay.UseCases.Infrastructure;
using System;

using System.Linq;

namespace Blinkay.UseCases.UseCases
{
    public class InsertionUseCase : ThreadManagedDBUseCase
    {
  
            public InsertionUseCase(int iNumRegistries, int iNumThreads, string connectionname) : base(iNumRegistries,  iNumThreads, connectionname)
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

            }
        }



    }
}
