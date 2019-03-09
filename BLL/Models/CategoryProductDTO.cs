using System.Collections.Generic;

namespace BLL.Models
{
    public class CategoryProductDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductDTO> Product { get; set; }
    }
}
