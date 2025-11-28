using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Cammara.Base
{
    public class CameraConfig
    {
        public CameraParameter CameraParameter { get; set; } = new CameraParameter();
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Direction { get; set; }
    }
}
