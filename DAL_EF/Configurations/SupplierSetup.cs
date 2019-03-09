using System.Data.Entity.ModelConfiguration;
using DAL_EF.Entities;

namespace DAL_EF.Configurations
{
    class SupplierSetup : EntityTypeConfiguration<Supplier>
    {
        public SupplierSetup()
        {
            this.HasKey(e => e.Id);
            this.Property(e => e.CompanyName).HasMaxLength(50).IsRequired();

            this.HasMany(e => e.Products)
            .WithRequired(e => e.Supplier)
            .WillCascadeOnDelete(false);
        }
    }
}
