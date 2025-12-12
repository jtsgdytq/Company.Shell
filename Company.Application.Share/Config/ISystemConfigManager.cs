using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Config
{
    public interface ISystemConfigManager
    {
        CameraConfig LeftCameraConfig { get; }


        CameraConfig RightCameraConfig { get; }

        SoftwareConfig SoftwareConfig { get; }

        void Save();
    }
}
