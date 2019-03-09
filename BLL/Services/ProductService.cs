using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;
using BLL.Infrastructure;
//using DAL_Adonet.Entities;
//using DAL_Adonet.Interfaces;
using DAL_EF.Interfaces;
using DAL_EF.Entities;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
                this.unitOfWork = unitOfWork;

            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<Product, ProductDTO>();
                con.CreateMap<Supplier, SupplierDTO>();
                con.CreateMap<CategoryProduct, CategoryProductDTO>();
            }
            );

            mapper = config.CreateMapper();
        }

        public void Create(ProductDTO product)
        {
            if (product == null)
                throw new ValidationException("Cannot create the nullable instance of Product");

            try
            {
                Product newProduct = mapper.Map<Product>(product);
                unitOfWork.Products.Insert(newProduct);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot create an instance of Product", ex);
            }
        }

        public void Update(ProductDTO product)
        {
            try
            {
                Product newProduct = mapper.Map<Product>(product);
                unitOfWork.Products.Update(newProduct);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot update an instance of Product", ex);
            }
        }

        public void Delete(int productId)
        {
            try
            {
                unitOfWork.Products.Delete(productId);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot delete an instance of Product", ex);
            }
        }

        public ProductDTO GetById(int id)
        {
            ProductDTO productDTO;
            try
            {
                productDTO = mapper.Map<ProductDTO>(unitOfWork.Products.FindWithId(id));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instance of Product", ex);
            }

            return productDTO;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            IEnumerable<ProductDTO> productDTOs;
            try
            {
                productDTOs = mapper.Map<IEnumerable<ProductDTO>>(unitOfWork.Products.FindAll());
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instances of Product", ex);
            }

            return productDTOs;
        }

        public IEnumerable<ProductDTO> GetProductsFromCategory(string category)
        {
            try
            {
                return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(
                    unitOfWork.Products.FindAll().
                    Join(unitOfWork.CategoriesProducts.FindAll(),
                    x => x.CategoryId, y => y.Id, (x, y) => new { Prod = x, Cat = y }).
                    Where(x => x.Cat.CategoryName == category).
                    Select(x => x.Prod));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get all an instances of Product", ex);
            }
        }

        public IEnumerable<ProductDTO> GetProductsFromSupplier(string supplier)
        {
            try
            {
                return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(
                    unitOfWork.Products.FindAll().
                    Join(unitOfWork.Suppliers.FindAll(),
                    x => x.SupplierId, y => y.Id, (x, y) => new { Prod = x, Sup = y }).
                    Where(x => x.Sup.CompanyName == supplier).
                    Select(x => x.Prod));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get all an instances of Product", ex);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
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
