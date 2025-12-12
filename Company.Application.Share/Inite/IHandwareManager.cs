using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Company.Application.Share.Inite
{
    /// <summary>
    /// 硬件管理接口
    /// </summary>
    public interface IHandwareManager
    {
        bool Initialized { get; }
        Task<IniteResulte> InitAsync();

         ICamera LeftCamera { get; }

         ICamera RightCamera { get; }

    }
}
