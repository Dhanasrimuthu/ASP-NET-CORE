using storedetail.model.domain;

namespace storedetail.Repositories
{
    public interface IStoreRepository
    {
        Task<Store>CreateAsync(Store store);
        Task<List<Store>> GetAllAsync(string? filterOn = null, Guid? filterQuery = null);
        Task<Store?> UpdateAsync(Guid id,Store store);
        Task<Store?> GetByIdAsync(Guid id);
        Task<Store?> DeleteAsync(Guid id);

    }
}
