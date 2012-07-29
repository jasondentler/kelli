using Ninject.Modules;

namespace KelliPokerPlanning
{
    public class ManagerModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IAccountManager>().To<AccountManager>();
        }
    }
}
