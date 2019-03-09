namespace DAL_EF.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public virtual CategoryProduct CategoriesProducts { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
