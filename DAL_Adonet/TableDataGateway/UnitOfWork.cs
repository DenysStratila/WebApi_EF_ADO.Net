using System;
using DAL_Adonet.Interfaces;

namespace DAL_Adonet.TableDataGateway
{
    public class UnitOfWork : IUnitOfWork
    {
        private CategoryProductGateway categoriesProductsGateway;
        private ProductGateway productGateway;
        private SupplierGateway suppliersGateway;

        private SqlContext context;

        public UnitOfWork(string connectionString)
        {
            context = new SqlContext(connectionString);
        }

        public ICategoryProductGateway CategoriesProducts
        {
            get
            {
                if (categoriesProductsGateway == null)
                {
                    categoriesProductsGateway = new CategoryProductGateway(context);
                }
                return categoriesProductsGateway;
            }
        }

        public ISupplierGateway Suppliers
        {
            get
            {
                if (suppliersGateway == null)
                {
                    suppliersGateway = new SupplierGateway(context);
                }
                return suppliersGateway;
            }
        }

        public IProductGateway Products
        {
            get
            {
                if (productGateway == null)
                {
                    productGateway = new ProductGateway(context);
                }
                return productGateway;
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