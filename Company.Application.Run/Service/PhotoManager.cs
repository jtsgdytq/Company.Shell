using Company.Application.Share.Inite;
using Company.Application.Share.Run;
using Company.Core.Models;
using Company.Hardware.Cammara.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Run.Service
{
    class PhotoManager : ReactiveUI.ReactiveObject, IPhotoManager
    {
        public ICamera Camera { get; set; }

        public BItmapGDI Current { get; set; }

        public BItmapGDI Left { get; set; }

        public BItmapGDI Right { get; set; }
        public IHandwareManager HandwareManager { get; set; }

        public PhotoManager(IHandwareManager handwareManager)
        {
            HandwareManager = handwareManager;
            //获取图像
            Left = new BItmapGDI(HandwareManager.LeftCamera.Width, HandwareManager.LeftCamera.Height);
            Right = new BItmapGDI(HandwareManager.RightCamera.Width, HandwareManager.RightCamera.Height);

            Current = Left;

            HandwareManager.LeftCamera.OnGrabbed += LeftCamera_OnGrabbed;
            HandwareManager.LeftCamera.ImageU8C1Observable.Subscribe(LeftCamera_OnGrabbed);
           
            HandwareManager.RightCamera.OnGrabbed += RightCamera_OnGrabbed;
            HandwareManager.RightCamera.ImageU8C1Observable.Subscribe(RightCamera_OnGrabbed);
        }

        private void RightCamera_OnGrabbed(ImageU8C1 c)
        {
            Right.WritePixle(c, 0, 0);
        }

        private void LeftCamera_OnGrabbed(ImageU8C1 obj)
        {
            Left.WritePixle(obj, 0, 0);
        }
        /// <summary>
        /// 设置图像切换
        /// </summary>
        /// <param name="CamearType"></param>

        public void SetPhoto(CameraType CamearType)
        {
           switch (CamearType)
            {
                case CameraType.left:
                    Current = Left;
                    Camera = HandwareManager.LeftCamera;
                    break;
                case CameraType.right:
                    Current = Right;
                    Camera = HandwareManager.RightCamera;
                    break;
                default:
                    break;
            }
        }
    }
}
