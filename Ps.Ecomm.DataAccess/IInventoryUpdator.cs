namespace Ps.Ecomm.DataAccess
{
    public interface IInventoryUpdator
    {
        Task UpdateAsync(int productId, int quantity);
    }
}