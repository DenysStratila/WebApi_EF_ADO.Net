using System;
using System.Data.SqlClient;
using DAL_Adonet.Interfaces;

namespace DAL_Adonet
{
    public class SqlContext : ISqlContext
    {
        private SqlConnection connection;
        private SqlTransaction transaction;

        public SqlContext(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        public void CreateCommand(SqlCommand command)
        {
            command.Connection = connection;
            command.Transaction = transaction;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        public void SaveChanges()
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction has already been commited. Check your transaction handling!");

            transaction.Commit();
            transaction = null;
        }
    }
}