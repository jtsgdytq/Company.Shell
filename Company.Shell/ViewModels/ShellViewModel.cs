using Prism.Regions;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Ioc;
using System.Linq;
using Company.Core.Models;
using Prism.Events;
using Company.Application.Share.EventModels;
using Company.DataBase.Base.Models;
using System.Windows;
using Company.Application.Initialize.Views;
using Prism.Modularity;

namespace Company.Shell.ViewModels
{
    
    public class ShellViewModel : ReactiveObject
    {
        public ICommand LoadedCommand { get; }
        public IRegionManager RegionManager { get; }
        public IEventAggregator EventAggregator { get; }

        public IModuleManager ModuleManager { get; }


        public ShellViewModel(IRegionManager regionManager, IContainerProvider container,IEventAggregator eventAggregator,IModuleManager moduleManager)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            ModuleManager = moduleManager;
            LoadedCommand = ReactiveCommand.Create(OnLoadCommand);
           
        }

        private void OnLoadCommand()
        {
            RegionManager.RequestNavigate(Names.ShellRegion ,Names.LoginView); 
            EventAggregator.GetEvent<LoginEvent>().Subscribe(OnloginSusess,ThreadOption.UIThread);
            EventAggregator.GetEvent<IniteEvent>().Subscribe(OnIniteSusess, ThreadOption.UIThread);
        }

        private void OnIniteSusess()
        {
            RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
        }

        private void OnloginSusess(UserEntity entity)
        {
            ModuleManager.LoadModule("IniteModule");
            RegionManager.RequestNavigate(Names.ShellRegion,Names.IniteView);
        }
    }
}