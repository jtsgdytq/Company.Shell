using Company.Application.Share.Inite;
using Company.Application.Share.Run;
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

        public IPhotoManager PhotoManager { get; set; }

        public RunViewModel(IHandwareManager handwareManager,IPhotoManager photoManager)
        {
            PhotoManager = photoManager;
        }


    }
}
