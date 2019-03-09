using System.Collections.Generic;
using DAL_Adonet.Entities;

namespace DAL_Adonet.Interfaces
{
    public interface IProductGateway
    {
        int Insert(Product product);

        int Update(Product product);

        int Delete(int id);

        IEnumerable<Product> FindAll();

        Product FindWithId(int id);
    }
}
