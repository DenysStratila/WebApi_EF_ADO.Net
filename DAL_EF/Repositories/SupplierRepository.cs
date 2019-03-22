using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL_EF.Interfaces;
using DAL_EF.Entities;

namespace DAL_EF.Repositories
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private SqlContext context;

        public SupplierRepository(SqlContext context)
        {
            this.context = context;
        }

        public void Insert(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
        }

        public void Update(Supplier supplier)
        {
            Supplier _supplier = FindWithId(supplier.Id);
            if (_supplier != null)
                context.Entry(_supplier).CurrentValues.SetValues(supplier);
            // Does not work if we are using Lazy Load.
            // context.Entry(supplier).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Supplier _supplier = context.Suppliers.Find(id);
            if (_supplier != null)
                context.Suppliers.Remove(_supplier);
        }

        public IEnumerable<Supplier> FindAll()
        {
            return context.Suppliers;
        }

        public IEnumerable<Supplier> Find(Expression<Func<Supplier, Boolean>> predicate)
        {
            return context.Suppliers.Where(predicate);
        }

        public Supplier FindWithId(int id)
        {
            return context.Suppliers.SingleOrDefault(d => d.Id == id);
        }
    }
}