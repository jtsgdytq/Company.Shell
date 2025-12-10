using Company.Core.Helpers;
using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using Company.Logger;
using MvCamCtrl.NET;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MvCamCtrl.NET.MyCamera;

namespace Company.Hardware.Cammara.Hik
{
    public class CameraHik_CS050_60GM :ReactiveUI.ReactiveObject ,ICamera
    {
        private MyCamera MyCamera { get; }  // 海康相机对象
        private MyCamera.MV_CC_DEVICE_INFO DeviceInfo;  // 设备信息
        private MyCamera.cbOutputExdelegate MyCameraHandler;  // 回调函数委托

        private CameraConfig CameraConfig; // 相机配置

        private IntPtr IntPtr = IntPtr.Zero; // 图像数据指针

        public ImageU8C1 ImageU8C1 { get; set; }

        public IObservable<ImageU8C1> ImageU8C1Observable { get; set; }

        public ImageU8C3 ImageU8C3 { get; set; }

        public IObservable<ImageU8C3> ImageU8C3Observable { get; set; }

        public int Width { get; } = 2448;

        public int Height { get; } = 2048;

        public bool Initialized { get; set; }

        public bool Connected { get; set; }

        public string Message { get; set; }

        public event Action<ImageU8C1> OnGrabbed;

        private AutoResetEvent wait =new AutoResetEvent(false);  // 同步信号


        public CameraHik_CS050_60GM()
        {
            MyCamera=new MyCamera();
            ImageU8C1Observable=this.WhenAnyValue(x => x.ImageU8C1); // 创建ImageU8C1的可观察对象
            ImageU8C3Observable =this.WhenAnyValue(x => x.ImageU8C3); // 创建ImageU8C3的可观察对象

        }

        public void Close()
        {
           var result=MyCamera.MV_CC_StopGrabbing_NET(); // 停止采集图像
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"停止采集图像失败,错误码{result}");
            }
            else
            {
                Logs.LogInfo("停止采集图像成功");
            }
            result=MyCamera.MV_CC_CloseDevice_NET(); // 关闭相机
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"关闭相机失败,错误码{result}");
            }
            else
            {
                Logs.LogInfo("关闭相机成功");
            }
            result=MyCamera.MV_CC_DestroyDevice_NET(); // 销毁相机
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"销毁相机失败,错误码{result}");
            }
            else
            {
                Logs.LogInfo("销毁相机成功");
            }
            if(IntPtr!=IntPtr.Zero)
            {
                Marshal.FreeHGlobal(IntPtr); // 释放图像内存
                IntPtr = IntPtr.Zero;
            }
            Initialized = false;
            Connected= false;

        }

        public float GetExposureTime()
        {
            throw new NotImplementedException();
        }

        public float GetGain()
        {
            throw new NotImplementedException();
        }

        public bool Init(CameraConfig cameraConfig)
        {
            try
            {
                CameraConfig = cameraConfig;
                if(Initialized) // 已初始化
                {
                    return true;
                }

                MyCamera.MV_CC_DEVICE_INFO_LIST deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST(); // 设备列表

                
                int result=MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);

                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"相机枚举失败,错误码{result}");
                    return false;
                }

                if (deviceList.nDeviceNum == 0)
                {
                    Logs.LogError("没有找到相机设备");
                    return false;
                }
                
                bool isFound = false;

                for (int i = 0; i < deviceList.nDeviceNum; i++)
                {
                    // 获取设备信息通过反射
                    DeviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));

                    if (DeviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE) // 获取网口类型相机
                    {
                        // 通过相机IP地址匹配
                       
                        MyCamera.MV_GIGE_DEVICE_INFO info = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(DeviceInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_CC_DEVICE_INFO));
                        uint ip1 = info.nCurrentIp & 0xff000000 >> 24;
                        uint ip2 = info.nCurrentIp & 0x00ff0000 >> 16;
                        uint ip3 = info.nCurrentIp & 0x0000ff00 >> 8;
                        uint ip4 = info.nCurrentIp & 0x000000ff;
                        string ip = $"{ip1}.{ip2}.{ip3}.{ip4}";

                        if (ip == cameraConfig.IpAddress) // IP地址匹配成功
                        {
                            isFound = true;
                            break;
                        }                  
                    }
                }
                if (!isFound)
                {
                    Logs.LogError("没有找到匹配IP地址的相机设备");
                    return false;
                }

                // 打开相机,开始采集图像

                // 创建相机
                result = MyCamera.MV_CC_CreateDevice_NET(ref DeviceInfo);

                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"相机创建失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("相机创建成功");
                }

                // 打开相机
                result = MyCamera.MV_CC_OpenDevice_NET();
                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"相机打开失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("相机打开成功");
                }
                // 设置触发模式为软件触发

                result = MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON); // 软触发模式打开
                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"设置触发模式失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("设置触发模式成功");
                }

                //设置触发源为软件触发

                result = MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"设置触发源失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("设置触发源成功");
                }

                //开始抓图，并设置拉流策略

                MyCamera.MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImages);

                // 注册回调函数
                MyCameraHandler = new MyCamera.cbOutputExdelegate(OnGrab);          
                result = MyCamera.MV_CC_RegisterImageCallBackEx_NET(MyCameraHandler, IntPtr.Zero);
                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"注册回调函数失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("注册回调函数成功");
                }

                // 开始采集图像
                result = MyCamera.MV_CC_StartGrabbing_NET();
                if (result != MyCamera.MV_OK)
                {
                    Logs.LogError($"开始采集图像失败,错误{result}");
                    return false;
                }
                else
                {
                    Logs.LogInfo("开始采集图像成功");
                }

                Initialized = true;
                Connected= true;

            }
            catch (Exception e)
            {
                Logs.LogError($"相机初始化失败：{e.Message}");

            }
            return false;
        }

        private void OnGrab(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                if(IntPtr==IntPtr.Zero)  // 分配图像内存
                {
                    IntPtr=Marshal.AllocHGlobal(new IntPtr(Width*Height));
                }

                // 将图像数据拷贝到预分配的内存中
                MemeryHelper.CopyMemory(IntPtr, pData, Width * Height);

               ImageU8C1 = new ImageU8C1(IntPtr, Width, Height);

                // 触发图像采集事件 将图像数据传递出去
                OnGrabbed?.Invoke(ImageU8C1);

            
            }
            catch ( Exception e)
            {
                Logs.LogError(e);
            }
            finally
            {
                wait.Set();
            }

        }

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 相机曝光时间设置
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetExposureTime(float value)
        {
             var result= MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", value);
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"设置曝光时间失败,错误码{result}");
                return false;
            }
            else
            {
                Logs.LogInfo($"设置曝光时间成功,曝光时间{value}us");
                return true;
            }

        }
        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetGain(float value)
        {
            var result= MyCamera.MV_CC_SetFloatValue_NET("Gain", value);
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"设置增益失败,错误码{result}");
                return false;
            }
            else
            {
                Logs.LogInfo($"设置增益成功,增益{value}dB");
                return true;
            }
        }
        /// <summary>
        /// 相机触发设置
        /// </summary>
        /// <returns></returns>
        public bool Trigger()
        {
            Stopwatch stopwatch  = Stopwatch.StartNew();

            var result= MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware"); // 软触发
            if (result != MyCamera.MV_OK)
            {
                Logs.LogError($"相机触发失败,错误码{result}");
                return false;
            }
            else
            {
                Logs.LogInfo($"相机触发成功,耗时{stopwatch.ElapsedMilliseconds}ms");
            }
            var r= wait.WaitOne(1000); // 等待图像采集完成，超时1秒

            if(!r)
            {
                Logs.LogError("相机采集图像超时");
                return false;
            }
            return r;
        }
    }
}
