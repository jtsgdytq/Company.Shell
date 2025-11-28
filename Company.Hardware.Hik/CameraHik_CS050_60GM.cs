using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Cammara.Hik
{
    public class CameraHik_CS050_60GM : ICamera
    {
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

        public void Close()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
