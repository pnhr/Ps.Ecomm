using Ps.Ecomm.Models;

namespace Ps.Ecomm.DataAccess
{
    public interface IProductProvider
    {
        Task<Product[]> Get();
    }
}