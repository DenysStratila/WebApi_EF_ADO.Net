using System;
using System.Collections.Generic;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ISupplierService: IDisposable
    {
        void Create(SupplierDTO supplier);
        void Update(SupplierDTO supplier);
        void Delete(int supplierId);

        SupplierDTO GetById(int id);

        IEnumerable<SupplierDTO> GetAll();
        IEnumerable<SupplierDTO> GetSuppliersByCategory(string category);
        IEnumerable<SupplierDTO> GetSuppliersWhereCategoryMax();
    }
}
