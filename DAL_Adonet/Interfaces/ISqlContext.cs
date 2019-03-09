using System;
using System.Data.SqlClient;

namespace DAL_Adonet.Interfaces
{
    public interface ISqlContext: IDisposable
    {
        void CreateCommand(SqlCommand command);
        void SaveChanges();
    }
}
