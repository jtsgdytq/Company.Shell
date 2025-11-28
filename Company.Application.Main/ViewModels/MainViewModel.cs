using Company.Application.Share.EventModels;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Company.Application.Main.ViewModels
{
    public class MainViewModel:ReactiveUI.ReactiveObject
    {
        public ICommand MainViewLoadedCommand { get; }


        public ICommand LogoutCommand { get; }


        public IRegionManager RegionManager { get; }

        public IEventAggregator EventAggregator { get; }

        public MainViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            MainViewLoadedCommand = ReactiveUI.ReactiveCommand.Create(OnMainViewLoaded);
            LogoutCommand = ReactiveUI.ReactiveCommand.Create(OnLogout);
        }

        private void OnLogout()
        {
            EventAggregator.GetEvent<LogoutEvent>().Publish();
        }

        private void OnMainViewLoaded()
        {
            // 使用带回调的 RequestNavigate 以便捕获错误/模块加载问题
            RegionManager.RequestNavigate("MainViewRegion", "RunView");
           
        }
    }
}
