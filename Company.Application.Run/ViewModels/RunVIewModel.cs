using Company.Application.Share.Inite;
using Company.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Run.ViewModels
{
    public class RunViewModel
    {
        public BItmapGDI BItmapGDI { get; set; }

        public RunViewModel(IHandwareManager handwareManager)
        {
            BItmapGDI = new BItmapGDI(handwareManager.LeftCamera.Width, handwareManager.LeftCamera.Height);
            BItmapGDI.WritePixle(handwareManager.LeftCamera.ImageU8C1, 0, 0);
        }
    }
}
