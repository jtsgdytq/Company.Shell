using Company.Core.Config;
using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Cammara.Hik
{
    [Module(ModuleName = Names.CameraHikModule, OnDemand = true)] //延迟加载
    public class HikCemaraModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册海康相机在硬件模式下注册相机
            if (!ConfigManager.NoHardwareMode)
            {
                //注册两个相机实例
                containerRegistry.RegisterSingleton<ICamera, CameraHik_CS050_60GM>(CameraType.left.ToString());
                containerRegistry.RegisterSingleton<ICamera, CameraHik_CS050_60GM>(CameraType.right.ToString());
            }
        }
    }
}
