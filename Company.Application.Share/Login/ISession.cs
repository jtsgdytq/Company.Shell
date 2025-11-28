using Company.DataBase.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.Share.Login
{
    public interface ISession
    {
        Visibility Visibility { get; }
        UserEntity CurrentUser { get; set; }
        Window MainWindow { get; }

        bool MessageBox(string message, MessageBoxButton messageBoxButton = MessageBoxButton.OK);
    }
}
