using System;

namespace DAL_Adonet.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ISupplierGateway Suppliers { get; }

        ICategoryProductGateway CategoriesProducts { get; }

        IProductGateway Products { get; }

        void Save();
    }
}
