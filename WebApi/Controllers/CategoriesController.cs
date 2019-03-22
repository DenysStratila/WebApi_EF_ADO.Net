using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class CategoriesController : ApiController
    {
        private ICategoryService categoryService;
        private ISupplierService supplierService;
        private IProductService productService;
        private IMapper mapper;

        public CategoriesController(ISupplierService supplierService, IProductService productService, ICategoryService categoryService)
        {
            this.supplierService = supplierService;
            this.productService = productService;
            this.categoryService = categoryService;
            var config = new MapperConfiguration(con =>
            {
                con.CreateMap<ProductDTO, ProductModelView>();
                con.CreateMap<SupplierDTO, SupplierModelView>();
                con.CreateMap<CategoryProductDTO, CategoryProductModelView>();
            });

            mapper = config.CreateMapper();
        }

        // GET api/categories
        public IEnumerable<CategoryProductModelView> GetAllCategories()
        {
            return mapper.Map<IEnumerable<CategoryProductDTO>, IEnumerable<CategoryProductModelView>>(categoryService.GetAll());
        }

        // GET api/categories/id
        public IHttpActionResult GetCategories(int id)
        {
            var category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = mapper.Map<CategoryProductDTO, CategoryProductModelView>(category);

            return Ok(model);
        }

        // POST api/categories/id
        public IHttpActionResult PostCategory([FromBody]CategoryProductModelView category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return BadRequest();
            }

            var model = new CategoryProductDTO
            {
                CategoryName = category.CategoryName,
                Product = mapper.Map<ICollection<ProductModelView>, ICollection<ProductDTO>>(category.Product)
            };

            categoryService.Create(model);

            return Ok(new { Message = $"The new category {category.CategoryName} has created" });
        }

        // PUT api/categories/id
        public IHttpActionResult PutCategory(int id, [FromBody]CategoryProductModelView category)
        {
            var catOld = categoryService.GetById(id);
            if (catOld == null)
            {
                return NotFound();
            }

            if (category == null)
            {
                return BadRequest();
            }

            var newCat = new CategoryProductDTO
            {
                Id = id,
                CategoryName = category.CategoryName,
                Product = mapper.Map<ICollection<ProductModelView>, ICollection<ProductDTO>>(category.Product)
            };
            categoryService.Update(newCat);

            return Ok(new { Message = $"A category {category.CategoryName} has updated" });
        }

        // DELETE api/categories/id
        public IHttpActionResult DeleteCategory(int id)
        {
            if (categoryService.GetById(id) == null)
            {
                return NotFound();
            }
            categoryService.Delete(id);

            return Ok(new { Message = $"A category with Id={id} has deleted" });
        }

        // GET api/categories/{id}/products 
        [HttpGet]
        [Route("categories/{id}/products")]
        public IHttpActionResult GetProductsByCategoryId(int id)
        {
            var category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var products = productService.GetProductsFromCategory(category.CategoryName);
            var models = mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductModelView>>(products);

            return Ok(models);
        }

        // GET api/categories/{id}/suppliers 
        [HttpGet]
        [Route("categories/{id}/suppliers")]
        public IHttpActionResult GetSuppliersByCategoryId(int id)
        {
            var category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var suppliers = supplierService.GetSuppliersByCategory(category.CategoryName);
            var models = mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierModelView>>(suppliers);

            return Ok(models);
        }

        // GET api/categories/max/suppliers 
        [HttpGet]
        [Route("categories/max/suppliers")]
        public IEnumerable<SupplierModelView> Get()
        {
            var suppliers = supplierService.GetSuppliersWhereCategoryMax();
            var models = mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierModelView>>(suppliers);

            return models;
        }
    }
}
