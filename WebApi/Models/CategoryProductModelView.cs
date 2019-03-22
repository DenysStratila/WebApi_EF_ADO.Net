using System.Collections.Generic;

namespace WebApi.Models
{
    public class CategoryProductModelView
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductModelView> Product { get; set; }
    }
}