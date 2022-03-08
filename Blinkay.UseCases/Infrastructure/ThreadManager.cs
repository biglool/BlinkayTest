
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Blinkay.UseCases.Infrastructure
{
   public class ThreadManager
    {
        private int _iNumThreads;
        protected ThreadManager(int iNumThreads)
        {
            _iNumThreads = iNumThreads;
        }
        protected int executeThreads(ThreadStart threadDelegate)
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
            return (int)stopWatch.Elapsed.TotalMilliseconds;

        }
    }
}
