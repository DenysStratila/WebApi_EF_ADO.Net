using System.Collections.Generic;
using DAL_Adonet.Entities;

namespace DAL_Adonet.Interfaces
{
    public interface ICategoryProductGateway
    {
        int Insert(CategoryProduct category);

        int Update(CategoryProduct category);

        int Delete(int id);

        IEnumerable<CategoryProduct> FindAll();

        CategoryProduct FindWithId(int id);
    }
}
