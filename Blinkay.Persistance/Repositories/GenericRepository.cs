using Blinkay.Domain;
using Blinkay.Persistance.Infrastructure;
using NHibernate;
using System;

namespace Blinkay.Persistance.Respositories
{
    public class GenericRepository : Repository<Generic>
    {
        public GenericRepository(string connectionString)
            : base(connectionString)
        {
        }
        public void UpdateRandom()
        {
            using (ISession session = NHibernateHelper.OpenSession(base.GetDbString()))
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    // update
                    Generic ent = base.GetRandom();
                    ent.SetRandomText();
                    base.Update(ent);
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw e;
                }
            }

        }
    }
}

