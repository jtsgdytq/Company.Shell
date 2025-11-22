using Company.DataBase.Base.Models;
using Company.DataBase.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Sqlite
{
    public class UserRepository :IUserRepository
    {
       
        SqliteDbContext db = new SqliteDbContext();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(UserEntity entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return SaveChanges();
        }
        /// <summary>
        /// 查找所有用户
        /// </summary>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public List<UserEntity> FindAll(string Keyword)
        {
          return db.Users.Where(u => u.UserName.Contains(Keyword) || u.Password.Contains(Keyword)).ToList();
        }
        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity Get(int id)
        {
            return db.Users.Find(id);
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAll()
        {
            return db.Users.ToList();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(UserEntity entity)
        {
           db.Entry(entity).State = System.Data.Entity.EntityState.Added;
              return SaveChanges();
        }

        public int SaveChanges()
        {
           return db.SaveChanges();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public UserEntity Select (string Keyword)
        {
           return db.Users.ToList().FirstOrDefault(u => u.UserName == Keyword);
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(UserEntity entity)
        {
           db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
              return SaveChanges();
        }
    }
}
