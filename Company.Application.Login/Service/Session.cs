using Company.Application.Share.Login;
using Company.DataBase.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.Login.Service
{
    public class Session : ReactiveUI.ReactiveObject, ISession
    {
        public Visibility Visibility { get; set; }

        public UserEntity CurrentUser { get ;set ; }

        public Window MainWindow => System.Windows.Application.Current.MainWindow;

        public bool MessageBox(string message, MessageBoxButton messageBoxButton = MessageBoxButton.OK)
        {
            return false;
        }
    }
}
