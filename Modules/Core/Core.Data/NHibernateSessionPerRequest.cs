using System;
using System.Web;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Core.Data
{
    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory _sessionFactory;

        // Constructs our HTTP module
        static NHibernateSessionPerRequest()
        {
            _sessionFactory = CreateSessionFactory();
        }

        // Returns our NHibernate session factory
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("hibernateConn")))
                .Mappings(m =>
                      m.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()) ) //.ExportTo(@"C:\"))
                .ExposeConfiguration(c =>
                    {
                        BuildSchema(c);
                        c.Properties[NHibernate.Cfg.Environment.CurrentSessionContextClass] = "web";
                    })
                  .BuildSessionFactory();    
        }

     

        // Drops and creates the database shema
        private static void BuildSchema(Configuration cfg)
        {
            new SchemaExport(cfg);//.Create(true, true);
        }

        // Initializes the HTTP module
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        // Disposes the HTTP module
        public void Dispose() { }

        // Returns the current session
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        // Opens the session, begins the transaction, and binds the session
        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = _sessionFactory.OpenSession();

            session.BeginTransaction();

            CurrentSessionContext.Bind(session);
        }

        // Unbinds the session, commits the transaction, and closes the session
        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session == null) return;

            try
            {
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }
    }
}
