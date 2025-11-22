using Company.Application.Login.ViewModels;
using Company.Application.Login.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Diagnostics;
using System.Linq;

namespace Company.Application.Login
{
    [Module(ModuleName = "LoginModule", OnDemand = false)]
    public class LoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
          containerRegistry.RegisterForNavigation<LoginView>();
        }
    }
}