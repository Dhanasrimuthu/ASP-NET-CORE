using storedetail.model.domain;
using storedetail.Model.Dtos;

namespace storedetail.Repositories
{
    public interface IProductRepository
    {
        Task<Product>CreateAsync(Product Product);
        Task<List<UserProductData[]>> GetUserDataAsync(Guid? userId, string? status);
        Task<List<Product>>GetAllAsync(string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 40);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> UpdateAsync(Guid id, Product product);
        Task<Product?> DeleteAsync(Guid id); 
    }
}
