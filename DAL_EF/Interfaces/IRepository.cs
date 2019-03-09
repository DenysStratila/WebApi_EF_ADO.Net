using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace DAL_EF.Interfaces
{
    public interface IRepository<T> where T: class
    {
        void Insert(T item);

        void Update(T item);

        void Delete(int id);

        IEnumerable<T> FindAll();

        IEnumerable<T> Find(Expression<Func<T, Boolean>> predicate);

        T FindWithId(int id);
    }
}
