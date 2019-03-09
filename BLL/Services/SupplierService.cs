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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SupplierService(IUnitOfWork unitOfWork)
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

        public void Create(SupplierDTO supplier)
        {
            if (supplier == null)
                throw new ValidationException("Cannot create the nullable instance of Supplier");

            try
            {
                Supplier newSupplier = mapper.Map<Supplier>(supplier);
                unitOfWork.Suppliers.Insert(newSupplier);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot create an instance of Supplier", ex);
            }
        }

        public void Update(SupplierDTO supplier)
        {
            try
            {
                Supplier newSupplier = mapper.Map<Supplier>(supplier);
                unitOfWork.Suppliers.Update(newSupplier);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot update an instance of Supplier", ex);
            }
        }

        public void Delete(int supplierId)
        {
            try
            {
                unitOfWork.Suppliers.Delete(supplierId);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot delete an instance of Supplier", ex);
            }
        }

        public SupplierDTO GetById(int id)
        {
            SupplierDTO supplierDTO;
            try
            {
                supplierDTO = mapper.Map<SupplierDTO>(unitOfWork.Suppliers.FindWithId(id));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instance of Supplier", ex);
            }

            return supplierDTO;
        }

        public IEnumerable<SupplierDTO> GetAll()
        {
            IEnumerable<SupplierDTO> supplierDTOs;
            try
            {
                supplierDTOs = mapper.Map<IEnumerable<SupplierDTO>>(unitOfWork.Suppliers.FindAll());
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get an instances of Supplier", ex);
            }

            return supplierDTOs;
        }

        public IEnumerable<SupplierDTO> GetSuppliersByCategory(string category)
        {
            try
            {
                return mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(
                    unitOfWork.Products.FindAll().
                    Join(unitOfWork.CategoriesProducts.FindAll(),
                    x => x.CategoryId, y => y.Id, (x, y) => new { Prod = x, Cat = y }).
                    Join(unitOfWork.Suppliers.FindAll(),
                    x => x.Prod.SupplierId, y => y.Id, (x, y) => new { x.Prod, x.Cat, Sup = y }).
                    Where(x => x.Cat.CategoryName == category).
                    Select(x => x.Sup).Distinct().OrderBy(x => x.CompanyName));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get all an instances of Supplier", ex);
            }
        }

        public IEnumerable<SupplierDTO> GetSuppliersWhereCategoryMax()
        {
            try
            {
                var tableRes = unitOfWork.Products.FindAll().
                    Select(x => new { Supp = x.SupplierId, Cat = x.CategoryId }).Distinct().
                    GroupBy(x => x.Supp).
                    Select(g => new { Sup = g.Key, Count = g.Count() });

                var maxRes = tableRes.Max(x => x.Count);

                var supRes = tableRes.Where(x => x.Count == maxRes).Select(x => x.Sup);

                return mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(
                    unitOfWork.Suppliers.FindAll().Where(x => supRes.Contains(x.Id)));
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot get all an instances of Supplier", ex);
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
