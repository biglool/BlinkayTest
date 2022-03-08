using Blinkay.UseCases.UseCases;
using Serilog;
using System;
using System.Web.Http;

namespace Blinkay.WebApi
{


    /// <summary>
    /// Benchmark controller
    /// </summary>
    public class BenchmarkController : ApiController
    {
        private ILogger Logger { get; }

        public BenchmarkController(ILogger logger)
        {

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Insert new registries into Mysql
        /// </summary>
        /// <param name="iNumRegistries">The number of insertions per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("MySQLInsertion")]
        public int MySQLInsertion(int iNumRegistries, int iNumThreads) {

            InsertionUseCase usecase = new InsertionUseCase(iNumRegistries, iNumThreads, Constants.Settings.MySqlConn);

            return usecase.Execute();
        }
        /// <summary>
        /// Update registies into Mysql
        /// </summary>
        /// <param name="iNumRegistries">The number of updates per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("MySQLSelectPlusUpdate")]
        public int MySQLSelectPlusUpdate(int iNumRegistries, int iNumThreads)
        {
            SelectPlusUpdateUseCase usecase = new SelectPlusUpdateUseCase(iNumRegistries, iNumThreads, Constants.Settings.MySqlConn);

            return usecase.Execute();
        }

        /// <summary>
        /// Insert and Update registies into Mysql
        /// </summary>
        /// <param name="iNumRegistries">The number of inserts and updates per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("MySQLSelectPlusUpdatePlusInsertion")]
        public int MySQLSelectPlusUpdatePlusInsertion(int iNumRegistries, int iNumThreads)
        {
            SelectPlusUpdatePlusInsertionUseCase usecase = new SelectPlusUpdatePlusInsertionUseCase(iNumRegistries, iNumThreads, Constants.Settings.MySqlConn);

            return usecase.Execute();
        }
        /// <summary>
        /// Insert new registries into Postgress
        /// </summary>
        /// <param name="iNumRegistries">The number of insertions and updates per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("PGInsertion")]
        public int PGInsertion(int iNumRegistries, int iNumThreads)
        {
            InsertionUseCase usecase = new InsertionUseCase(iNumRegistries, iNumThreads, Constants.Settings.PgConn);

            return usecase.Execute();
        }
        /// <summary>
        /// Update registies into Postgress
        /// </summary>
        /// <param name="iNumRegistries">The number of updates per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("PGSelectPlusUpdate")]
        public int PGSelectPlusUpdate(int iNumRegistries, int iNumThreads)
        {
            SelectPlusUpdateUseCase usecase = new SelectPlusUpdateUseCase(iNumRegistries, iNumThreads, Constants.Settings.PgConn);

            return usecase.Execute();
        }
        /// <summary>
        /// Insert and Update registies into Postgress
        /// </summary>
        /// <param name="iNumRegistries">The number of insertions and updates per thread</param>
        /// <param name="iNumThreads">The number of threads created</param>
        /// <returns>Time of execution in ms</returns>
        [HttpPost]
        [Route("PGSelectPlusUpdatePlusInsertion")]
        public int PGSelectPlusUpdatePlusInsertion(int iNumRegistries, int iNumThreads)
        {
            SelectPlusUpdatePlusInsertionUseCase usecase = new SelectPlusUpdatePlusInsertionUseCase(iNumRegistries, iNumThreads, Constants.Settings.PgConn);

            return usecase.Execute();
        }


    }
}