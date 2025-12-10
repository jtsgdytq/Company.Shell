using Company.Core.Config;
using Company.Hardware.Cammara.Base;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Camera.Emulation
{
    [Module(ModuleName ="CameraEmulationModule", OnDemand = true)] //延迟加载
    public class CameraEmulationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           if(ConfigManager.NoHardwareMode)
            {
                containerRegistry.Register<ICamera,CameraEmulation>(CameraType.left.ToString());
                containerRegistry.Register<ICamera, CameraEmulation>(CameraType.right.ToString());
            }
        }
    }
}
