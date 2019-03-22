using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL_Adonet.Interfaces;
using DAL_Adonet.Entities;

namespace DAL_Adonet.TableDataGateway
{
    public class ProductGateway : IProductGateway
    {
        private ISqlContext context { get; set; }

        public ProductGateway(ISqlContext context)
        {
            this.context = context;
        }

        public int Insert(Product product)
        {
            int id = 0;

            if (product != null)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Products VALUES (@ProductName, @CategoryId, @SupplierId)"))
                {
                    command.Parameters.AddWithValue("ProductName", product.ProductName);
                    command.Parameters.AddWithValue("CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("SupplierId", product.SupplierId);

                    context.CreateCommand(command);
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return id;
        }

        public int Update(Product product)
        {
            int changes = 0;

            if (product != null)
            {
                using (SqlCommand command = new SqlCommand("UPDATE Products SET ProductName = @ProductName, @CategoryId = CategoryId, @SupplierId = SupplierId WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", product.Id);
                    command.Parameters.AddWithValue("ProductName", product.ProductName);
                    command.Parameters.AddWithValue("CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("SupplierId", product.SupplierId);

                    context.CreateCommand(command);
                    changes = command.ExecuteNonQuery();
                }
            }

            return changes;
        }

        public int Delete(int id)
        {
            int changes = 0;

            if (id > 0)
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", id);

                    context.CreateCommand(command);
                    changes = command.ExecuteNonQuery();
                }
            }

            return changes;
        }

        public IEnumerable<Product> FindAll()
        {
            List<Product> list = null;

            using (SqlCommand command = new SqlCommand("SELECT * FROM Products"))
            {
                context.CreateCommand(command);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    list = new List<Product>();

                    while (dataReader.Read())
                    {
                        list.Add(new Product()
                        {
                            Id = (int)dataReader[0],
                            ProductName = (string)dataReader[1],
                            CategoryId = (int)dataReader[2],
                            SupplierId = (int)dataReader[3]
                        });
                    }
                }
            }

            return list;
        }

        public Product FindWithId(int id)
        {
            Product product = null;

            if (id > 0)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", id);

                    context.CreateCommand(command);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                            product = new Product()
                            {
                                Id = (int)dataReader[0],
                                ProductName = (string)dataReader[1],
                                CategoryId = (int)dataReader[2],
                                SupplierId = (int)dataReader[3]
                            };
                    }
                }
            }

            return product;
        }
    }
}