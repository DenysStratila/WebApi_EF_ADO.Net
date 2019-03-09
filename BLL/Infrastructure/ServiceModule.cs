using Ninject.Modules;
using BLL.Interfaces;
using BLL.Services;
//using DAL_Adonet.Interfaces;
//using DAL_Adonet.TableDataGateway;
using DAL_EF.Interfaces;
using DAL_EF.Repositories;

namespace BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;

        public ServiceModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().WithConstructorArgument(connectionString);
            Bind<ICategoryService>().To<CategoryService>().InSingletonScope();
            Bind<IProductService>().To<ProductService>().InSingletonScope();
            Bind<ISupplierService>().To<SupplierService>().InSingletonScope();
        }
    }
}
