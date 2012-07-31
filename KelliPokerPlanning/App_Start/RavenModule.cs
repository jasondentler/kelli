using System.Configuration;
using System.Web.Mvc;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Raven.Client;
using Raven.Client.Document;

namespace KelliPokerPlanning.App_Start
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

            this.BindFilter<RavenFilter>(FilterScope.Controller, 0);
        }
    }
}