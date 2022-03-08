
using Blinkay.Domain;
using Blinkay.Persistance.Respositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace NhibernateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQLSelectPlusUpdateUseCase usecase = new MySQLSelectPlusUpdateUseCase(3, 2);

            Console.WriteLine();
            Console.WriteLine(usecase.execute());
            Console.ReadLine();
        }
    }
    class ThreadManager
    {
        private int _iNumThreads;
        public ThreadManager(int iNumThreads)
        {
            _iNumThreads = iNumThreads;
        }
        public double executeThreads(ThreadStart threadDelegate)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Thread> thlist = new List<Thread>();
            for (int i = 0; i < _iNumThreads; i++)
            {
                thlist.Add(new Thread(threadDelegate));
            }

            for (int i = 0; i < _iNumThreads; i++)
            {
                thlist[i].Start();
                thlist[i].Join();
            }

            stopWatch.Stop();
            return stopWatch.Elapsed.TotalMilliseconds;

        }
    }
    class MySQLSelectPlusUpdateUseCase : ThreadManager
    {
        private int _iNumRegistries;
        private string connectionName = "MysqlConnection";

        public MySQLSelectPlusUpdateUseCase(int iNumRegistries, int iNumThreads) : base(iNumThreads)
        {
            _iNumRegistries = iNumRegistries;

        }

        public double execute()
        {
            return base.executeThreads(new ThreadStart(basetask));
        }
        private void basetask()
        {
            for (int i = 0; i < _iNumRegistries; i++)
            {
                GenericRepository repo = new GenericRepository(connectionName);

                Generic genericEntity = new Generic();
                genericEntity.SetRandomText();
                repo.Add(genericEntity);
                repo.UpdateRandom();

                Console.WriteLine(repo.GetAll().Count());

            }
        }

    }
}

