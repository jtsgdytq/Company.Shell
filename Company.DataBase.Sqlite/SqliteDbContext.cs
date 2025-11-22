using Company.DataBase.Base.Models;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Sqlite
{
    public class SqliteDbContext:DbContext
    {
        public SqliteDbContext() : base("SqliteDbContext")
        {

        }
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //定义创建Sqlite工具类
            var sqliteConnect = new SqliteCreateDatabaseIfNotExists<SqliteDbContext>(modelBuilder);
            //执行
            System.Data.Entity.Database.SetInitializer(sqliteConnect);
        }
    }
}
