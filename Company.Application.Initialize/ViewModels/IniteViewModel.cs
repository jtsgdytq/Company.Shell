using Company.Application.Share.Inite;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Company.Application.Initialize.ViewModels
{
    public class IniteViewModel:ReactiveObject
    {
        
        public ICommand IniteLoadedCommand { get; }
        public IHandwareManager handwareManager { get; }

        


        public IniteViewModel(IHandwareManager manager)
        {
            handwareManager = manager;
            IniteLoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
        }

        private async void OnLoadedCommand()
        {
            await Task.Delay(2000);
            handwareManager.InitAsync();
        }
    }
}
