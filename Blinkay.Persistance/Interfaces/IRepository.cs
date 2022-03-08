using System.Collections.Generic;

namespace Blinkay.Persistance.Interfaces
{
    interface IRepository<T>
    {
        string GetDbString();
        int Add(T entity);
        void Update(T entity);
        T GetById(int id);
        List<T> GetAll();
        T GetRandom();
    }
}
