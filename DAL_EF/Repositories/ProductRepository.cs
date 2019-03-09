using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DAL_EF.Interfaces;
using DAL_EF.Entities;

namespace DAL_EF.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private SqlContext context;

        public ProductRepository(SqlContext context)
        {
            this.context = context;
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product _product = context.Products.Find(id);
            if (_product != null)
                context.Products.Remove(_product);

            context.SaveChanges();
        }

        public IEnumerable<Product> FindAll()
        {
            return context.Products;
        }

        public IEnumerable<Product> Find(Expression<Func<Product, Boolean>> predicate)
        {
            return context.Products.Where(predicate);
        }

        public Product FindWithId(int id)
        {
            return context.Products.Find(id);
        }
    }
}