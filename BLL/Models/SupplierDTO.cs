using System.Collections.Generic;

namespace BLL.Models
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
    }
}
