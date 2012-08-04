using Ninject.Modules;

namespace KelliPokerPlanning.App_Start
{
    public class ManagerModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IAccountManager>().To<AccountManager>();
            Kernel.Bind<ISessionManager>().To<SessionManager>();
        }
    }
}
