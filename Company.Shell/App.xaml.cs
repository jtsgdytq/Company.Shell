using Company.Application.Login;
using Company.Application.Login.Views;
using Company.Core;
using Company.DataBase.Sqlite;
using Company.Logger;
using Company.Shell.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Shell
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Logs.LogError("未处理异常", e.ExceptionObject as Exception);
            };

            DispatcherUnhandledException += (s, e) =>
            {
                Logs.LogError("UI线程未处理异常", e.Exception);
                e.Handled = true;
            };
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Logs.LogError("任务线程未处理异常", e.Exception);
                e.SetObserved();
            };
        }
        protected override Window CreateShell()
        {
            return new ShellView();
        }
        protected override void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CoreModel>(); // 注册核心模块
           
            

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog()
            {
                ModulePath = @"./"
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           

            Logs.LogInfo("程序启动");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logs.LogInfo("程序退出");
            base.OnExit(e);
        }
    }
}
