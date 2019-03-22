using System;
using System.Collections.Generic;
using BLL.Models;
using BLL.Interfaces;
using BLL.Infrastructure;
using AutoMapper;
using DAL_Adonet.Entities;
using DAL_Adonet.Interfaces;
//using DAL_EF.Interfaces;
//using DAL_EF.Entities;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
                this.unitOfWork = unitOfWork;

            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<CategoryProductDTO, CategoryProduct>();
                con.CreateMap<CategoryProduct, CategoryProductDTO>();

                con.CreateMap<ProductDTO, Product>();
                con.CreateMap<Product, ProductDTO>();
            }
            );

            mapper = config.CreateMapper();
        }

        public void Create(CategoryProductDTO categoryProduct)
        {
            if (categoryProduct == null)
                throw new ValidationException("Cannot create the nullable instance of CategoryProduct");

            try
            {
                CategoryProduct newCategoryProduct = mapper.Map<CategoryProduct>(categoryProduct);
                unitOfWork.CategoriesProducts.Insert(newCategoryProduct);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot create an instance of CategoryProduct", ex);
            }
        }
            
        public void Update(CategoryProductDTO categoryProduct)
        {
            try
            {
                CategoryProduct newCategoryProduct = mapper.Map<CategoryProduct>(categoryProduct);
                unitOfWork.CategoriesProducts.Update(newCategoryProduct);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot update an instance of CategoryProduct", ex);
            }
        }

        public void Delete(int categoryProductId)
        {
            try
            {
                unitOfWork.CategoriesProducts.Delete(categoryProductId);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot delete an instance of CategoryProduct", ex);
            }
        }

        public CategoryProductDTO GetById(int id)
        {
            CategoryProductDTO categoryProductDTO;
            try
            {
                categoryProductDTO = mapper.Map<CategoryProductDTO>(unitOfWork.CategoriesProducts.FindWithId(id));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instance of CategoryProduct", ex);
            }

            return categoryProductDTO;
        }

        public IEnumerable<CategoryProductDTO> GetAll()
        {
            IEnumerable<CategoryProductDTO> categoryProductDTOs;
            try
            {
                categoryProductDTOs = mapper.Map<IEnumerable<CategoryProductDTO>>(unitOfWork.CategoriesProducts.FindAll());
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instances of CategoryProduct", ex);
            }

            return categoryProductDTOs;
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