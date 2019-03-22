using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductService productService;
        private IMapper mapper;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
            var config = new MapperConfiguration(con =>
            {
                con.CreateMap<ProductDTO, ProductModelView>();
            });

            mapper = config.CreateMapper();
        }

        // GET api/products
        public IEnumerable<ProductModelView> GetAllProducts()
        {
            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductModelView>>(productService.GetAll());
        }

        // GET api/products/id
        public IHttpActionResult GetProduct(int id)
        {
            var product = productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ProductDTO, ProductModelView>(product);

            return Ok(model);
        }

        // POST api/products/id
        public IHttpActionResult PostProduct([FromBody]ProductModelView product)
        {
            if (!ModelState.IsValid || product == null)
            {
                return BadRequest();
            }

            var model = new ProductDTO
            {
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId
            };

            productService.Create(model);

            return Ok(new { Message = $"The new product {product.ProductName} has created" });
        }

        // PUT api/products/id
        public IHttpActionResult PutProduct(int id, [FromBody]ProductModelView product)
        {
            var prodOld = productService.GetById(id);
            if (prodOld == null)
            {
                return NotFound();
            }

            if (product == null)
            {
                return BadRequest();
            }

            var newProd = new ProductDTO
            {
                Id = id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            };
            productService.Update(newProd);

            return Ok(new { Message = $"A product {product.ProductName} has updated" });
        }

        // DELETE api/products/id
        public IHttpActionResult DeleteProduct(int id)
        {
            if (productService.GetById(id) == null)
            {
                return NotFound();
            }
            productService.Delete(id);

            return Ok(new { Message = $"A product with Id={id} has deleted" });
        }
    }
}
