using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        }

        public void Update(Product product)
        {
            Product _product = FindWithId(product.Id);
            if (_product != null)
                context.Entry(_product).CurrentValues.SetValues(product);
            // Does not work if we are using Lazy Load.
            // context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product _product = context.Products.Find(id);
            if (_product != null)
                context.Products.Remove(_product);
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
            return context.Products.SingleOrDefault(d => d.Id == id);
        }
    }
}