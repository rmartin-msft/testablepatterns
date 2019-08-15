using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSalesApp.Repositories
{

    public interface IRepository<T>
    {
        T GetById(int id);
        T[] GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>> expression);
    }
}
