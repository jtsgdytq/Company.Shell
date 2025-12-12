using Company.Application.Share.Config;
using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config.Modle
{
    public class SystemConfigModle
    {

        public SoftwareConfig  SoftwareConfig { get; set; } = new SoftwareConfig();
        public CameraConfig LeftCameraConfig { get; set; } = new CameraConfig()
        {
            IpAddress = "198.98.0.1",
            Direction = CameraType.left.ToString(),
        };

        public CameraConfig RightCameraConfig { get; set; } = new CameraConfig()
        {
            IpAddress = "198.98.0.2",
            Direction = CameraType.right.ToString(),
        };
    }
}
