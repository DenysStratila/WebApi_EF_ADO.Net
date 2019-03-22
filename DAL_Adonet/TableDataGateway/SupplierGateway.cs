using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL_Adonet.Interfaces;
using DAL_Adonet.Entities;

namespace DAL_Adonet.TableDataGateway
{
    public class SupplierGateway : ISupplierGateway
    {
        private ISqlContext context { get; set; }

        public SupplierGateway(ISqlContext context)
        {
            this.context = context;
        }

        public int Insert(Supplier supplier)
        {
            int id = 0;

            if (supplier != null)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Suppliers VALUES (@CompanyName)"))
                {
                    command.Parameters.AddWithValue("CompanyName", supplier.CompanyName);

                    context.CreateCommand(command);
                    id = Convert.ToInt32(command.ExecuteScalar());
                    //context.SaveChanges();
                }
            }
            //context.Dispose();

            return id;
        }

        public int Update(Supplier supplier)
        {
            int changes = 0;

            if (supplier != null)
            {
                using (SqlCommand command = new SqlCommand("UPDATE Suppliers SET CompanyName = @SupplierName WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", supplier.Id);
                    command.Parameters.AddWithValue("SupplierName", supplier.CompanyName);

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
                using (SqlCommand command = new SqlCommand("DELETE FROM Suppliers WHERE Id = @Id"))
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

        public IEnumerable<Supplier> FindAll()
        {
            List<Supplier> list = null;

            using (SqlCommand command = new SqlCommand("SELECT * FROM Suppliers"))
            {
                context.CreateCommand(command);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {

                    list = new List<Supplier>();

                    while (dataReader.Read())
                    {
                        list.Add(new Supplier()
                        {
                            Id = (int)dataReader[0],
                            CompanyName = (string)dataReader[1]
                        });
                    }
                }
            }
            //context.Dispose();

            return list as IEnumerable<Supplier>;
        }

        public Supplier FindWithId(int id)
        {
            Supplier supplier = null;

            if (id > 0)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Suppliers WHERE Id = @Id"))
                {
                    command.Parameters.AddWithValue("Id", id);

                    context.CreateCommand(command);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                            supplier = new Supplier()
                            {
                                Id = (int)dataReader[0],
                                CompanyName = (string)dataReader[1]
                            };
                    }
                }
            }
            //context.Dispose();

            return supplier;
        }
    }
}