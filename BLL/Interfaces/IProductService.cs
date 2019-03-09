using System;
using System.Collections.Generic;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        void Create(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(int productId);

        ProductDTO GetById(int id);

        IEnumerable<ProductDTO> GetAll();
        IEnumerable<ProductDTO> GetProductsFromCategory(string category);
        IEnumerable<ProductDTO> GetProductsFromSupplier(string supplier);
    }
}
