using Company.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Cammara.Base
{
    /// <summary>
    /// 表示一台工业相机的接口
    /// </summary>
    public interface ICamera
    {
        event Action<ImageU8C1> OnGrabbed;
        ImageU8C1 ImageU8C1 { get; }
        IObservable<ImageU8C1> ImageU8C1Observable { get; }
        ImageU8C3 ImageU8C3 { get; }
        IObservable<ImageU8C3> ImageU8C3Observable { get; }
        /// <summary>
        /// 相机宽度（像素）
        /// </summary>
        int Width { get; }
        /// <summary>
        /// 相机高度（像素）
        /// </summary>
        int Height { get; }
        /// <summary>
        /// 是否加载成功
        /// </summary>
        bool Initialized { get; }
        /// <summary>
        /// 连接状态 
        /// </summary>
        bool Connected { get; }
        /// <summary>
        /// 运行消息
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 相机初始化
        /// </summary>
        /// <param name="cameraConfig"></param>
        /// <returns></returns>
        bool Init(CameraConfig cameraConfig);
        /// <summary>
        /// 设备增益
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetGain(float value);
        /// <summary>
        /// 时间曝光时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetExposureTime(float value);
        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <returns></returns>
        float GetExposureTime();
        /// <summary>
        /// 获取增益
        /// </summary>
        /// <returns></returns>
        float GetGain();
        /// <summary>
        /// 拍照
        /// </summary>
        /// <returns></returns>
        bool Trigger();
        /// <summary>
        /// 关闭相机
        /// </summary>
        void Close();
        /// <summary>
        /// 加载仿真图
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool Load(string filename);
    }
}
