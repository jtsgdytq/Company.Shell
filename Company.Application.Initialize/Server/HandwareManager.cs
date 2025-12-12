using Company.Application.Share.Config;
using Company.Application.Share.EventModels;
using Company.Application.Share.Inite;
using Company.Hardware.Cammara.Base;
using ControlzEx.Standard;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Company.Application.Initialize.Server
{
    public class HandwareManager : IHandwareManager
    {
        public bool Initialized { get;  set; }
        public ICamera LeftCamera{ get;  set; }
        public ICamera RightCamera { get;  set; }
        public IEventAggregator EventAggregator { get; }
        public ISystemConfigManager SystemConfigManager { get; }
        public HandwareManager(IEventAggregator eventAggregator,ISystemConfigManager systemConfigManager, IContainerProvider containerProvider)
        {
            EventAggregator= eventAggregator;
            SystemConfigManager= systemConfigManager;
            // 获取相机实例
            LeftCamera = containerProvider.Resolve<ICamera>(CameraType.left.ToString());
            RightCamera= containerProvider.Resolve<ICamera>(CameraType.right.ToString());

           
        }

        public  async Task<IniteResulte> InitAsync()
        {
            if(Initialized)   throw new Exception("硬件已初始化");

            await Task.Delay(2000);
            IniteResulte result= new IniteResulte();

            // 初始化硬件
            Task<bool> leftCameraTask = Task.Run(() => LeftCamera.Init(SystemConfigManager.LeftCameraConfig));
            Task<bool> rightCameraTask = Task.Run(() => RightCamera.Init(SystemConfigManager.RightCameraConfig));
           
            var boolArray = await Task.WhenAll(leftCameraTask, rightCameraTask);
            bool temp = boolArray.All(p => p);//判断所有子任务的加载结果是否都为true
            if (temp)
            {
                Initialized = true;
                result.Message = "初始化硬件成功";
                result.IsSuccess = true;
                EventAggregator.GetEvent<IniteEvent>().Publish();//通知主窗体跳转
                return result;
            }
            else
            {
                //其中有一些模块加载失败
                string msg = "以下硬件模块初始化失败\r\n";
                if (leftCameraTask.Result == false)
                {
                    msg += "左相机模块 ";
                    System.Diagnostics.Debug.WriteLine(LeftCamera.Message);
                }
                if (rightCameraTask.Result == false)
                {
                    msg += "右相机模块 ";
                    System.Diagnostics.Debug.WriteLine(RightCamera.Message);
                }
            
                return result;
            }
        }
    }
}
