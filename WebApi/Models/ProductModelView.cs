namespace WebApi.Models
{
    public class ProductModelView
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}