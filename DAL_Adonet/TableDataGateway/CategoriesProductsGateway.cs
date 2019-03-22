using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL_Adonet.Interfaces;
using DAL_Adonet.Entities;

namespace DAL_Adonet.TableDataGateway
{
    public class CategoryProductGateway : ICategoryProductGateway
    {
        private ISqlContext context { get; set; }

        public CategoryProductGateway(ISqlContext context)
        {
            this.context = context;
        }

        public int Insert(CategoryProduct category)
        {
            int id = 0;

            if (category != null)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO CategoriesProducts VALUES (@CategoryName)"))
                {
                    command.Parameters.AddWithValue("CategoryName", category.CategoryName);

                    context.CreateCommand(command);
                    id = Convert.ToInt32(command.ExecuteScalar());
                    //context.SaveChanges();
                }
            }
            //context.Dispose();

            return id;
        }

        public int Update(CategoryProduct category)
        {
            int changes = 0;

            if (category != null)
            {
                using (SqlCommand command = new SqlCommand("UPDATE CategoriesProducts SET CategoryName = @CategoryName WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", category.Id);
                    command.Parameters.AddWithValue("CategoryName", category.CategoryName);

                    context.CreateCommand(command);
                    changes = command.ExecuteNonQuery();
                    //context.SaveChanges();
                }
            }
            //context.Dispose();

            return changes;
        }

        public int Delete(int id)
        {
            int changes = 0;

            if (id > 0)
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM CategoriesProducts WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", id);

                    context.CreateCommand(command);
                    changes = command.ExecuteNonQuery();
                    //context.SaveChanges();
                }
            }
            //context.Dispose();

            return changes;
        }

        public IEnumerable<CategoryProduct> FindAll()
        {
            List<CategoryProduct> list = null;

            using (SqlCommand command = new SqlCommand("SELECT * FROM CategoriesProducts"))
            {
                context.CreateCommand(command);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    list = new List<CategoryProduct>();

                    while (dataReader.Read())
                    {
                        list.Add(new CategoryProduct()
                        {
                            Id = (int)dataReader[0],
                            CategoryName = (string)dataReader[1]
                        });
                    }
                }
            }
            //context.Dispose();

            return list;// as IEnumerable<CategoryProduct>;
        }

        public CategoryProduct FindWithId(int id)
        {
            CategoryProduct categoryProduct = null;

            if (id > 0)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM CategoriesProducts WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", id);

                    context.CreateCommand(command);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                            categoryProduct = new CategoryProduct()
                            {
                                Id = (int)dataReader[0],
                                CategoryName = (string)dataReader[1]
                            };
                    }
                }
            }
            //context.Dispose();

            return categoryProduct;
        }
    }
}