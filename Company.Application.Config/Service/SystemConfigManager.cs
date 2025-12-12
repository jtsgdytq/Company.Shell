using Company.Application.Config.Modle;
using Company.Application.Share.Config;
using Company.Core.Config;
using Company.Core.Enums;
using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config.Service
{
    public class SystemConfigManager : ReactiveUI.ReactiveObject, ISystemConfigManager
    {
        public SystemConfigModle Config;

        public IConfigManager ConfigManager;
        public CameraConfig LeftCameraConfig => Config.LeftCameraConfig;

        public CameraConfig RightCameraConfig => Config.RightCameraConfig;

        public SoftwareConfig SoftwareConfig => Config.SoftwareConfig;


        public SystemConfigManager(IConfigManager configManager)
        {
            ConfigManager = configManager;
            Load();
        }
        /// <summary>
        /// 加载配置
        /// </summary>
        private void Load()
        {
           Config=ConfigManager.Read<SystemConfigModle>(ConfigKey.systemConfig);
            if(Config == null)
            {
                Config = new SystemConfigModle();
                ConfigManager.Write(ConfigKey.systemConfig, Config);
            }

        }
        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
           ConfigManager.Write(ConfigKey.systemConfig, Config);
        }
    }
}
