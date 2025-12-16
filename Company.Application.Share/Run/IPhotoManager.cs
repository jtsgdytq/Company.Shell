using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Run
{
    public interface IPhotoManager
    {
        ICamera Camera { get; }

        BItmapGDI  Current { get;}

        void SetPhoto(CameraType CamearType);
    }
}
