using System.Collections.Generic;
using BLL.Models;
using System;

namespace BLL.Interfaces
{
    public interface ICategoryService: IDisposable
    {
        void Create(CategoryProductDTO categoryProduct);
        void Update(CategoryProductDTO categoryProduct);
        void Delete(int categoryProductId);

        CategoryProductDTO GetById(int id);

        IEnumerable<CategoryProductDTO> GetAll();
    }
}
