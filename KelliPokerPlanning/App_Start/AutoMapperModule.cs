using AutoMapper;
using KelliPokerPlanning.Models;
using Ninject.Modules;

namespace KelliPokerPlanning.App_Start
{
    public class AutoMapperModule : NinjectModule 
    {
        public override void Load()
        {
            Mapper.CreateMap<Settings, PokerSetup>();
            Mapper.CreateMap<PokerSetup, Settings>();
        }
    }
}