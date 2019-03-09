namespace BLL.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public CategoryProductDTO CategoryProduct { get; set; }
        public SupplierDTO Supplier { get; set; }
    }
}
