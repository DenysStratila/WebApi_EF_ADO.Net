using System.Collections.Generic;
using DAL_Adonet.Entities;

namespace DAL_Adonet.Interfaces
{
    public interface ISupplierGateway
    {
        int Insert(Supplier supplier);

        int Update(Supplier supplier);

        int Delete(int id);

        IEnumerable<Supplier> FindAll();

        Supplier FindWithId(int id);
    }
}
