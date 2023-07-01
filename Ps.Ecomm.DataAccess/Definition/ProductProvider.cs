using Dapper;
using Microsoft.Data.SqlClient;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.DataAccess.Definition
{
    public class ProductProvider : IProductProvider
    {
        private readonly string _connectionString;

        public ProductProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Product[]> Get()
        {
            using var connection = new SqlConnection(_connectionString);
            var data = await connection.QueryAsync<Product>(@"SELECT Id, Name, Description, Type FROM Product");
            return data.ToArray();
        }
    }
}