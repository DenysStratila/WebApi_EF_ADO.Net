using System.Data.Entity.ModelConfiguration;
using DAL_EF.Entities;

namespace DAL_EF.Configurations
{
    class CategoryProductSetup : EntityTypeConfiguration<CategoryProduct>
    {
        public CategoryProductSetup()
        {
            this.HasKey(e => e.Id);
            this.Property(e => e.CategoryName).HasMaxLength(50).IsRequired();

            this.ToTable("CategoriesProducts").HasMany(e => e.Products)
            .WithRequired(e => e.CategoriesProducts)
            .HasForeignKey(e => e.CategoryId)
            .WillCascadeOnDelete(false);
        }
    }
}
