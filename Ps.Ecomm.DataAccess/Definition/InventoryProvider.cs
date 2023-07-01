using Dapper;
using Microsoft.Data.SqlClient;
using Ps.Ecomm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ps.Ecomm.DataAccess.Definition
{
    public class InventoryProvider : IInventoryProvider
    {
        private readonly string _connectionString;

        public InventoryProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Inventory[]> Get()
        {
            using var connection = new SqlConnection(_connectionString);
            var data = await connection.QueryAsync<Inventory>(@"SELECT Id, Name, Quantity, ProductId FROM Inventory");
            return data.ToArray();
        }
    }
}
