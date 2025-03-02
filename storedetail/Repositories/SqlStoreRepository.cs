using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using storedetail.Data;
using storedetail.model.domain;
using storedetail.Model.Domain;
using System.Security.Claims;

namespace storedetail.Repositories
{
    public class SqlStoreRepository : IStoreRepository
    {
        private readonly storedetailDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SqlStoreRepository(storedetailDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<Store> CreateAsync(Store store)
        {
            await dbContext.Store.AddAsync(store);
            await dbContext.SaveChangesAsync();
            return store;
        }
        public  Task<List<Store>> GetAllAsync(string? filterOn = null, Guid? filterQuery = null)
        {
            IQueryable<Store> storeQuery = dbContext.Store.AsQueryable();


            if (!string.IsNullOrWhiteSpace(filterOn) && filterQuery != null)
            {
                if (filterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    storeQuery = storeQuery.Where(x => x.UserId == filterQuery.Value);
                }
            }
            return storeQuery.ToListAsync();
        }   

        public async Task<Store?> GetByIdAsync(Guid id)
        {
            return await dbContext.Store.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Store?> DeleteAsync(Guid id)
        {
            var existingStore = await dbContext.Store.FirstOrDefaultAsync(x => x.Id == id);
            IQueryable<Product> existingProduct =  dbContext.Product.Where(x => x.StoreId == id);

            if (existingStore == null)
            {
                return null;
            }
            dbContext.Store.Remove(existingStore);
            var productList =await existingProduct.ToListAsync();
            if (productList != null)
            {
                dbContext.Product.RemoveRange(productList);
            }
            await dbContext.SaveChangesAsync();
            return existingStore;
        }

        public async Task<Store?> UpdateAsync(Guid id, Store store)
        {
            var existingStore =await dbContext.Store.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingStore == null) 
            {
                return null;
            }
            existingStore.Name= store.Name;
            existingStore.Description= store.Description;

            await dbContext.SaveChangesAsync();
            return existingStore;
        }
    }
}
