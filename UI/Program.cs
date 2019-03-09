using System;
using System.Configuration;
using Ninject;
using BLL.Interfaces;
using BLL.Infrastructure;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardKernel ninject = new StandardKernel(new ServiceModule(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString));

            IProductService product = ninject.Get<IProductService>();
            ICategoryService category = ninject.Get<ICategoryService>();
            ISupplierService supplier = ninject.Get<ISupplierService>();

            #region Get the list of products of a given category.

            Console.WriteLine("\t\t<<<The list of products of Smartphones category>>>\n");
            var productsFromCategories = product.GetProductsFromCategory("Smartphones");
            foreach (var prod in productsFromCategories)
            {
                Console.WriteLine($"Product: \t{prod.ProductName}");
                Console.WriteLine($"Company: \t{supplier.GetById(prod.SupplierId).CompanyName}");

                Console.WriteLine(new string('-', 30));
            }

            #endregion

            #region Define all suppliers of products of a given category.

            Console.WriteLine("\t\t<<<Suppliers of products of a Computers category>>>\n");
            var suppliersByCategory = supplier.GetSuppliersByCategory("Computers");
            foreach (var sup in suppliersByCategory)
            {
                Console.WriteLine($"Supplier: \t{sup.CompanyName}");
            }
            Console.WriteLine(new string('-', 30));

            #endregion

            #region Define the list of products of a given supplier.

            Console.WriteLine("\t\t<<<The list of products of Dell supplier>>>\n");
            var productsFromSupplier = product.GetProductsFromSupplier("Dell");
            foreach (var prod in productsFromSupplier)
            {
                Console.WriteLine($"Product name: {prod.ProductName}");
            }
            Console.WriteLine(new string('-', 30));

            #endregion

            #region Define suppliers whose products fall into the maximum number of categories.

            Console.WriteLine("\t\t<<<Suppliers with the maximum number of categories>>>\n");
            var suppliersWhereCategoryMax = supplier.GetSuppliersWhereCategoryMax();
            foreach (var sup in suppliersWhereCategoryMax)
            {
                Console.WriteLine($"\tSupplier: {sup.CompanyName}\n");
                Console.WriteLine($"List of products:");
                var products = product.GetProductsFromSupplier(sup.CompanyName);
                foreach (var prod in products)
                {
                    Console.WriteLine($"=> {prod.ProductName}");
                }
                Console.WriteLine(new string('-', 30));
            }

            #endregion

            Console.ReadKey();
        }
    }
}