using Company.Application.Initialize.Server;
using Company.Application.Initialize.ViewModels;
using Company.Application.Initialize.Views;
using Company.Application.Share.Inite;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Initialize
{
    [Module(ModuleName = "IniteModule", OnDemand = true)] // 延迟加载模块
    public class IniteModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IniteView>("IniteView");
            containerRegistry.RegisterSingleton<IHandwareManager, HandwareManager>();
        }
    }
}
