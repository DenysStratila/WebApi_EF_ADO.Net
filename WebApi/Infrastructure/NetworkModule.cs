using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace WebApi.Infrastructure
{
    public class NetworkModule: NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IProductService>().To<ProductService>();
            Bind<ISupplierService>().To<SupplierService>();
        }
    }
}