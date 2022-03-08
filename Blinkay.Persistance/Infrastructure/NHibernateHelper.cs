using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Blinkay.Persistance.Infrastructure
{
    class NHibernateHelper
    {

        private static readonly Dictionary<string, ISessionFactory> _sessionFactorys = new Dictionary<string, ISessionFactory>();
        private static ISessionFactory _currentSessionFactory;
        static NHibernateHelper()
        {
            //loads a factory for each connectionstring in appconfig
            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                //TODO: look for a better filter
                if (!conn.Name.Equals("LocalSqlServer") && !conn.ConnectionString.Replace(" ", "").Equals(""))
                {
                    _sessionFactorys.Add(conn.Name, FluentConfigure(conn.Name));
                }

            }
        }
        public static ISession OpenSession(string dbconnection)
        {

            _currentSessionFactory = _sessionFactorys[dbconnection];
            return _currentSessionFactory.OpenSession();
        }
        public static ISession GetCurrentSession()
        {

            return _currentSessionFactory.GetCurrentSession();
        }
        public static void CloseSession()
        {

            _currentSessionFactory.Close();
        }
        public static void CloseSessionFactory()
        {
            if (_currentSessionFactory != null)
            {
                _currentSessionFactory.Close();
            }
        }

        public static ISessionFactory FluentConfigure(string dbconnection)
        {
            IPersistenceConfigurer config;
            //which database
            if (ConfigurationManager.ConnectionStrings[dbconnection].ProviderName.Equals("MySql"))
            {
                config = MySQLConfiguration.Standard.ConnectionString(cs => cs.FromConnectionStringWithKey(dbconnection));
            }
            else
            {
                config = PostgreSQLConfiguration.Standard.ConnectionString(cs => cs.FromConnectionStringWithKey(dbconnection));
            }

            return Fluently.Configure()

                .Database(config)
                //2nd level cache
                .Cache(
                    c => c.UseQueryCache()
                        .UseSecondLevelCache()
                        .ProviderClass<NHibernate.Cache.HashtableCacheProvider>())
                //find/set the mappings
                //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenericMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
    }
}
