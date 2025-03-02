using storedetail.model.domain;


namespace storedetail.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cart);
        Task<List<Cart>> GetAll(string? filterOn = null, Guid? filterQuery = null, string? status = null);
        Task<Cart?> GetByIdAsync(Guid id);
        Task<Cart?> UpdateAsync(Cart cart,Guid id);
        Task<Cart?> DeleteAsync(Guid id);
    }
}
