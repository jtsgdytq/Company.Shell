using Company.Application.Share.Inite;
using Company.Application.Share.Login;
using Company.Core.Models;
using Prism.Modularity;
using Prism.Regions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Company.Application.Initialize.ViewModels
{
    public class IniteViewModel:ReactiveObject
    {
        
        public ICommand IniteLoadedCommand { get; }

        public ICommand ReloadedCommand { get; }
        public ICommand LoadedCommand { get; }
        public IHandwareManager handwareManager { get; }

        public IRegionManager RegionManager { get; }

        public IModuleManager ModuleManager { get; }

        public ISession Session { get; }


        public IniteViewModel(IHandwareManager manager,IRegionManager regionManager,IModuleManager moduleManager,ISession session)
        {
            handwareManager = manager;
            RegionManager = regionManager;
            ModuleManager = moduleManager;
            Session = session;
            IniteLoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
            LoadedCommand= ReactiveCommand.Create(LoadCommand);
            ReloadedCommand= ReactiveCommand.CreateFromTask(OnLoadedCommand);
        }
        /// <summary>
        /// 跳过初始化直接加载主界面
        /// </summary>
        private void LoadCommand()
        {
            ModuleManager.LoadModule("MainModule");
            RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
        }
        private async Task OnLoadedCommand()
        {
            //最大化窗体
            Rect workArea = SystemParameters.WorkArea;
            Session.MainWindow.Left = (workArea.Width - Session.MainWindow.ActualWidth) / 2 + workArea.Left;
            Session.MainWindow.Top = (workArea.Height - Session.MainWindow.ActualHeight) / 2 + workArea.Top;
            Session.MainWindow.WindowState = WindowState.Maximized;
            if (!handwareManager.Initialized)
            {
                var result = await handwareManager.InitAsync();
                if (result.IsSuccess)
                {
                    ModuleManager.LoadModule("MainModule");
                    RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
                }
                else
                {
                    // 初始化失败处理
                    // 这里可以添加错误处理逻辑，例如显示错误消息等
                    throw new Exception("硬件初始化失败: " + result.Message);
                }
            }
        }
    }
}
