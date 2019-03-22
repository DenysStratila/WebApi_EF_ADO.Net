using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL_EF.Interfaces;
using DAL_EF.Entities;

namespace DAL_EF.Repositories
{
    public class CategoryRepository : IRepository<CategoryProduct>
    {
        private SqlContext context;

        public CategoryRepository(SqlContext context)
        {
            this.context = context;
        }

        public void Insert(CategoryProduct category)
        {
            context.CategoriesProducts.Add(category);
        }

        public void Update(CategoryProduct category)
        {
            CategoryProduct _category = FindWithId(category.Id);
            if (_category != null)
                context.Entry(_category).CurrentValues.SetValues(category);
            // Does not work if we are using Lazy Load.
            // context.Entry(category).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            CategoryProduct _category = context.CategoriesProducts.Find(id);
            if (_category != null)
                context.CategoriesProducts.Remove(_category);
        }

        public IEnumerable<CategoryProduct> FindAll()
        {
            return context.CategoriesProducts;
        }

        public IEnumerable<CategoryProduct> Find(Expression<Func<CategoryProduct, Boolean>> predicate)
        {
            return context.CategoriesProducts.Where(predicate);
        }

        public CategoryProduct FindWithId(int id)
        {
            return context.CategoriesProducts.SingleOrDefault(d => d.Id == id);
        }
    }
}