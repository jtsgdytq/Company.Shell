using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base.Models
{
    [Table (nameof(UserEntity))]
    public class UserEntity: EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public int Role { get; set; }
    }
}
