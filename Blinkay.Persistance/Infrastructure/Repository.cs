using Blinkay.Persistance.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blinkay.Persistance.Infrastructure
{

    public class Repository<T> : IRepository<T>
    {
        readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GetDbString()
        {
            return _connectionString;
        }
        public int Add(T entity)
        {
            int newId;

            using (ISession session = NHibernateHelper.OpenSession(_connectionString))
            using (var tx = session.BeginTransaction())
            {
                newId = (int)session.Save(entity);
                tx.Commit();
            }
            return newId;
        }


        public T GetById(int id)
        {
            T ent;

            using (ISession session = NHibernateHelper.OpenSession(_connectionString))
            using (var tx = session.BeginTransaction())
            {
                ent = session.Get<T>(id);

            }
            return ent;
        }

        public List<T> GetAll()
        {
            List<T> entitiesList;
            using (ISession session = NHibernateHelper.OpenSession(_connectionString))
            using (var tx = session.BeginTransaction())
            {
                IQueryable<T> entities = from ent in session.Query<T>()
                                         select ent;
                entitiesList = entities.ToList<T>();
            }

            return entitiesList;

        }

        public void Update(T entity)
        {

            using (ISession session = NHibernateHelper.OpenSession(_connectionString))
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    session.Update(entity);
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw e;
                }
            }


        }

        public T GetRandom()
        {
            T ent;
            using (ISession session = NHibernateHelper.OpenSession(_connectionString))
            using (var tx = session.BeginTransaction())
            {
                Random rnd = new Random();

                IQueryable<T> entities = from entiti in session.Query<T>()
                                         select entiti;
                 entities.ToList<T>();
                // get one random
                int skipTo = rnd.Next(0, entities.Count());
                entities.Skip(skipTo).Take(1).First();
                ent = entities.Skip(rnd.Next(0, entities.Count())).Take(1).First();
            }

            return ent;
        }
    }
}
