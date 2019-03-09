using System.Data.Entity;
using DAL_EF.Entities;
using DAL_EF.Configurations;

namespace DAL_EF
{
    public partial class SqlContext : DbContext
    {
        public SqlContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SqlContext>());
        }

        public SqlContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SqlContext>());
        }

        public virtual DbSet<CategoryProduct> CategoriesProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryProductSetup());
            modelBuilder.Configurations.Add(new ProductSetup());
            modelBuilder.Configurations.Add(new SupplierSetup());
        }
    }
}
