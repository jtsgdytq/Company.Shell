using Company.Core.Config;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core
{
    public class CoreModel : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IConfigManager, ConfigManager>();// 注册配置管理器
            if(containerRegistry.IsRegistered<IConfigManager>())
            {
                System.Diagnostics.Debug.WriteLine("IConfigManager注册成功");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("IConfigManager注册失败");
            }
        }
    }
}
