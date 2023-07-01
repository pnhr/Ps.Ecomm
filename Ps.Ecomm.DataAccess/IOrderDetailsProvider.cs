﻿using Ps.Ecomm.Models;

namespace Ps.Ecomm.DataAccess
{
    public interface IOrderDetailsProvider
    {
        Task<OrderDetail[]> Get();
    }
}