using Company.Core.Helpers;
using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using Company.Logger;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Camera.Emulation
{
    public class CameraEmulation :  ReactiveUI.ReactiveObject,ICamera
    {
        private IntPtr IntPtr = IntPtr.Zero; // 图像数据指针
        [Reactive]
        public ImageU8C1 ImageU8C1 { get; set; }

        public IObservable<ImageU8C1> ImageU8C1Observable { get; set; }
        [Reactive]
        public ImageU8C3 ImageU8C3 { get; set; }

        public IObservable<ImageU8C3> ImageU8C3Observable { get; set; }

        public int Width { get; } = 2448;

        public int Height { get; } = 2048;

        public bool Initialized { get; set; }

        public bool Connected { get; set; }

        public string Message { get; set; }

        public event Action<ImageU8C1> OnGrabbed;

        public CameraEmulation()
        {
            ImageU8C1Observable= this.WhenAnyValue(x => x.ImageU8C1);
            ImageU8C3Observable= this.WhenAnyValue(x => x.ImageU8C3);
        }


        public void Close()
        {
           Marshal.FreeHGlobal(IntPtr);
              IntPtr = IntPtr.Zero;
            Initialized = false;
            Connected = false;
            Logs.LogInfo("仿真相机关闭");
        }

        public float GetExposureTime()
        {
            throw new NotImplementedException();
        }

        public float GetGain()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 初始化相机
        /// </summary>
        /// <param name="cameraConfig"></param>
        /// <returns></returns>
        public bool Init(CameraConfig cameraConfig)
        {
            try
            {
                if (IntPtr == IntPtr.Zero)
                {
                    IntPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(Height * Width);
                }
                var filename = Path.Combine("images", $"{cameraConfig.Direction}.bmp");
                
                if (System.IO.File.Exists (filename))
                {
                   Bitmap bitmap= ImageHelper.Load (filename);
                    if(bitmap.Width != Width || bitmap.Height != Height)
                    {
                        Logs.LogError($"模拟相机初始化失败: 图像宽高不匹配. Expected {Width}x{Height}, got {bitmap.Width}x{bitmap.Height}");
                        return false;
                    }
                    // 锁定8位图的像素数据
                    var bitmapData = bitmap.LockBits(
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        ImageLockMode.ReadWrite, 
                        PixelFormat.Format8bppIndexed);
                    // 复制图像数据到内存指针
                    MemeryHelper.CopyMemory(IntPtr, bitmapData.Scan0, (uint)(Height * Width));
                    ImageU8C1 = new ImageU8C1(IntPtr, Width, Height);

                    // 解锁像素数据
                    bitmap.UnlockBits(bitmapData);

                }
                else
                {
                    Logs.LogError($"模拟相机初始化失败: 文件 {filename} 未被发现");
                    return false;
                }

            }
            catch ( Exception e)
            {
               
               Logs.LogError("模拟相机初始化失败", e);
                return false;
            }
          
            Initialized = true;
            Connected = true;
            return true;
        }

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }

        public bool SetExposureTime(float value)
        {
            throw new NotImplementedException();
        }

        public bool SetGain(float value)
        {
            throw new NotImplementedException();
        }

        public bool Trigger()
        {
            throw new NotImplementedException();
        }
    }
}
