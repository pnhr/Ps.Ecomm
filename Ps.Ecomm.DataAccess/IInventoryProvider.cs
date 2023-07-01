using Ps.Ecomm.Models;

namespace Ps.Ecomm.DataAccess
{
    public interface IInventoryProvider
    {
        Task<Inventory[]> Get();
    }
}