using storedetail.model.domain;
using storedetail.Model.Dtos;

namespace storedetail.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem>CreateAsync(CartItem cartItem);
        Task<List<CartItem>>GetAllAsync(string? filterOn = null, Guid? filterQuery = null, string? status = null);
        Task<List<UserProductData>> GetByIdAsync(Guid CartId);
        Task<CartItem?> UpdateAsync(Guid id, string status, Guid userId);
        /*Task<CartItem?> UpdateAsync(Guid id, CartItem cartItem);*/
        Task <CartItem?>DeleteAsync(Guid id);
        Task<bool>CartItemExistsAsync(Guid cartId, Guid productId);
    }
}
