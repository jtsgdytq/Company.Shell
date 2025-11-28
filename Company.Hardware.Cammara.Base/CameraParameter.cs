using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.Cammara.Base
{
    /// <summary>
    /// 相机参数 增益 曝光时间
    /// </summary>
    public class CameraParameter
    {
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain { get; set; } = 1.9f;
        /// <summary>
        /// 曝光微秒
        /// </summary>
        public float ExposureTime { get; set; } = 5000;
    }
}
