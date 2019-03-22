using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class SuppliersController : ApiController
    {
        private ISupplierService supplierService;
        private IProductService productService;
        private IMapper mapper;

        public SuppliersController(ISupplierService supplierService, IProductService productService)
        {
            this.supplierService = supplierService;
            this.productService = productService;
            var config = new MapperConfiguration(con =>
            {
                con.CreateMap<ProductDTO, ProductModelView>();
                con.CreateMap<SupplierDTO, SupplierModelView>();
                con.CreateMap<CategoryProductDTO, CategoryProductModelView>();
            });

            mapper = config.CreateMapper();
        }

        // GET api/suppliers
        public IEnumerable<SupplierModelView> GetAllSuppliers()
        {
            return mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierModelView>>(supplierService.GetAll());
        }

        // GET api/supplier/id
        public IHttpActionResult GetSupplier(int id)
        {
            var supplier = supplierService.GetById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var model = mapper.Map<SupplierDTO, SupplierModelView>(supplier);

            return Ok(model);
        }

        // POST api/suppliers/id
        public IHttpActionResult PostSupplier([FromBody]SupplierModelView supplier)
        {
            if (!ModelState.IsValid || supplier == null)
            {
                return BadRequest();
            }

            var model = new SupplierDTO
            {
                CompanyName = supplier.CompanyName,
                Products = mapper.Map<ICollection<ProductModelView>, ICollection<ProductDTO>>(supplier.Products)
            };

            supplierService.Create(model);

            return Ok(new { Message = $"The new supplier {supplier.CompanyName} has created" });
        }

        // PUT api/suppliers/id
        public IHttpActionResult PutSupllier(int id, [FromBody]SupplierModelView supplier)
        {
            var supOld = supplierService.GetById(id);
            if (supOld == null)
            {
                return NotFound();
            }

            if (supplier == null)
            {
                return BadRequest();
            }

            var newSup = new SupplierDTO
            {
                Id = id,
                CompanyName = supplier.CompanyName,
                Products = mapper.Map<ICollection<ProductModelView>, ICollection<ProductDTO>>(supplier.Products)
            };
            supplierService.Update(newSup);

            return Ok(new { Message = $"A supplier {supplier.CompanyName} has updated" });
        }

        // DELETE api/suppliers/id
        public IHttpActionResult DeleteSupplier(int id)
        {
            if (supplierService.GetById(id) == null)
            {
                return NotFound();
            }
            supplierService.Delete(id);

            return Ok(new { Message = $"A supplier with Id={id} has deleted" });
        }

        // GET api/suppliers/{id}/products 
        [HttpGet]
        [Route("suppliers/{id}/products")]
        public IHttpActionResult GetProductsBySupplierId(int id)
        {
            var supplier = supplierService.GetById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            var products = productService.GetProductsFromSupplier(supplier.CompanyName);
            var models = mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductModelView>>(products);

            return Ok(models);
        }
    }
}
