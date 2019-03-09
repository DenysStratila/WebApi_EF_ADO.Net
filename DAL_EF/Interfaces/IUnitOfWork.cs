using System;
using DAL_EF.Entities;

namespace DAL_EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Supplier> Suppliers { get; }

        IRepository<CategoryProduct> CategoriesProducts { get; }

        IRepository<Product> Products { get; }

        void Save();
    }
}
