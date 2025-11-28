using Company.Application.Share.EventModels;
using Company.Application.Share.Inite;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Initialize.Server
{
    public class HandwareManager : IHandwareManager
    {
        public bool Initialized { get;  set; }

        public IEventAggregator EventAggregator { get; }
        public HandwareManager(IEventAggregator eventAggregator)
        {
             EventAggregator= eventAggregator;
        }

        public  async Task<IniteResulte> InitAsync()
        {
            if(Initialized)   throw new Exception("硬件已初始化");

            await Task.Delay(5000);
            IniteResulte resulte = new IniteResulte();
            resulte.IsSuccess = true;
            resulte.Message = "硬件初始化成功";
            EventAggregator.GetEvent<IniteEvent>().Publish();
            return resulte;
        }
    }
}
