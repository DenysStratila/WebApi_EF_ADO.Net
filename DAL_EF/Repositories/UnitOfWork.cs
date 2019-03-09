using System;
using DAL_EF.Interfaces;
using DAL_EF.Entities;

namespace DAL_EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CategoryRepository categoryRepository;
        private ProductRepository productRepository;
        private SupplierRepository supplierRepository;

        private SqlContext context;

        public UnitOfWork(string connectionString)
        {
            context = new SqlContext(connectionString);
        }

        public IRepository<CategoryProduct> CategoriesProducts
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        public IRepository<Supplier> Suppliers
        {
            get
            {
                if (supplierRepository == null)
                {
                    supplierRepository = new SupplierRepository(context);
                }
                return supplierRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
