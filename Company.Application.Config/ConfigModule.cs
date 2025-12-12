using Company.Application.Config.Service;
using Company.Application.Share.Config;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config
{
    [Module(ModuleName = "ConfigModule", OnDemand = true)]
    public class ConfigModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           containerRegistry.RegisterSingleton<ISystemConfigManager, SystemConfigManager>();
        }
    }
}
