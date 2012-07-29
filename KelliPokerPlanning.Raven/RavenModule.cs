using System.Configuration;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Document;

namespace KelliPokerPlanning
{
    public class RavenModule : NinjectModule 
    {
        public override void Load()
        {
            var connStr = ConfigurationManager.AppSettings["RAVENHQ_CONNECTION_STRING"];
            var store = new DocumentStore();
            store.ParseConnectionString(connStr);
            store.Initialize();

            Kernel.Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();
        }
    }
}