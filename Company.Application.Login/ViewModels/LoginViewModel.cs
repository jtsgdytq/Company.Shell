using Company.Application.Share.EventModels;
using Company.Core.Enums;
using Company.DataBase.Base.Models;
using Company.DataBase.Base.Repositories;
using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace Company.Application.Login.ViewModels
{
    public class LoginViewModel:ReactiveObject
    {
        [Reactive]
        public UserEntity User { get; set; } = new UserEntity();

        public ICommand LoginCommand { get; }
        public IUserRepository UserRepository { get; }
       public IEventAggregator EventAggregator { get; }

        public LoginViewModel(IUserRepository userRepository,IEventAggregator eventAggregator)
        {

#if DEBUG
            User.UserName = "admin";
            User.Password = "123456";
#endif

            LoginCommand = ReactiveCommand.Create(OnLoginCommand);  
            UserRepository = userRepository;
           EventAggregator = eventAggregator;

        }

        private Task OnLoginCommand()
        {
           return Task.Run(() =>
           {
               var user = UserRepository.Select(User.UserName);
               if (user != null)
               {
                   if(user.Password == User.Password)
                   {
                     EventAggregator.GetEvent<LoginEvent>().Publish(user);
                   }
                   else
                   {
                      MessageBox.Show("账号密码错误");
                   }
               }
               else
               {
                   var newuser=new UserEntity()
                   {
                       UserName = User.UserName,
                       Password = User.Password,
                       Role = (int)RoleKey.访客,
                   };
                     UserRepository.Insert(newuser);
                        MessageBox.Show("新用户注册成功");
                   User = null; //清空输入框
               }
           } );
        }
    }
}
