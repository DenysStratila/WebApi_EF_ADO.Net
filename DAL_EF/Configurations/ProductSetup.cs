using System.Data.Entity.ModelConfiguration;
using DAL_EF.Entities;

namespace DAL_EF.Configurations
{
    class ProductSetup : EntityTypeConfiguration<Product>
    {
        public ProductSetup()
        {
            this.HasKey(e=>e.Id);
            this.Property(e => e.ProductName).HasMaxLength(50).IsRequired();
            this.Property(e => e.SupplierId).IsRequired();
            this.Property(e => e.CategoryId).IsRequired();
        }
    }
}
