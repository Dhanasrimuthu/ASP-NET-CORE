using storedetail.model.domain;

namespace storedetail.Repositories
{
    public interface IUserRepository
    {
        Task<List<Store>> GetAllAsync(string? filterOn = null, String? filterQuery = null);
    }
}
