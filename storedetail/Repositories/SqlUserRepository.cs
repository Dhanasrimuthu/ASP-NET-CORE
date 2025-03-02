using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using storedetail.Data;
using storedetail.model.domain;
using storedetail.Model.Domain;

namespace storedetail.Repositories
{
    public class SqlUserRepository:IUserRepository
    {
        private readonly storedetailDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SqlUserRepository(storedetailDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public Task<List<Store>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            throw new NotImplementedException();
        }
        /*  public Task<List<Store>> GetAllAsync(string? filterOn = null, Guid? filterQuery = null)
 {
     IQueryable<Store> storeQuery = dbContext.Store.AsQueryable();

     if (!string.IsNullOrWhiteSpace(filterOn) && filterQuery != null)
     {
         if (filterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
         {
             Guid filterGuid;
             if (Guid.TryParse(filterQuery.ToString(), out filterGuid))
             {
                 storeQuery = storeQuery.Where(x => x.UserId == filterGuid);
             }
         }
     }
     return storeQuery.ToListAsync();
 }*/

        /* public Task<List<Store>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
         {
             _userManager.fi
             var uservalue = dbContext.Users.AsQueryable();

             if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
             {
                 if (filterOn.Equals("userId", StringComparison.OrdinalIgnoreCase))
                 {
                     uservalue = v.Where(x => x.Category.Contains(filterQuery));
                 }
             }
         }*/
    }
}
