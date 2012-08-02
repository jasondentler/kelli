using System;
using System.Linq;
using AutoMapper;
using KelliPokerPlanning.Models;
using Ninject.Modules;

namespace KelliPokerPlanning.App_Start
{
    public class AutoMapperModule : NinjectModule 
    {
        public override void Load()
        {
            Mapper.CreateMap<Settings, PokerSetup>()
                .ForMember(ps => ps.Values, mo => mo.ResolveUsing(Join));

            Mapper.CreateMap<PokerSetup, Settings>()
                .ForMember(s => s.Values, mo => mo.ResolveUsing(Split));
        }

        private string Join(Settings s)
        {
            return s.JoinValues();
        }

        private string[] Split(PokerSetup ps)
        {
            return ps.SplitValues();
        }

    }
}