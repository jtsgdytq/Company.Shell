using Company.DataBase.Base.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.EventModels
{
    /// <summary>
    /// 登录事件
    /// </summary>
    public class LoginEvent:PubSubEvent<UserEntity> 
    {

    }
}
