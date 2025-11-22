using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class EntityBase:ReactiveObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Reactive]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自动增长
        [Key] //主键
        public int Id { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        [Reactive]
        public DateTime InsertTime { get; set; }= DateTime.Now;
    }
}
