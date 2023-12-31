﻿using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ps.Ecomm.DataAccess.Definition
{
    public class InventoryUpdator : IInventoryUpdator
    {
        private readonly string connectionString;

        public InventoryUpdator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task UpdateAsync(int productId, int quantity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            await connection.ExecuteAsync("uspUpdateInventory", 
                                            new { productId, quantity }, 
                                            commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
