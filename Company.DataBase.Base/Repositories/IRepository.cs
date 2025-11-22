using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataBase.Base.Repositories
{
    public interface IRepository <T> where T : class
    {
        T Get (int id);

        int Insert (T entity);

        int Update (T entity);

        int Delete (T entity);

        List<T> GetAll ();

        List<T> FindAll(string Keyword);

        T Select(string Keyword);

        int SaveChanges ();
    }
}
