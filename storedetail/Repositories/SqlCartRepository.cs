using Microsoft.EntityFrameworkCore;
using storedetail.Data;
using storedetail.model.domain;

namespace storedetail.Repositories
{
    public class SqlCartRepository : ICartRepository
    {
        private readonly storedetailDbContext dbContext;

        public SqlCartRepository(storedetailDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Cart>> GetAll(string? filterOn = null, Guid? filterQuery = null, string? status = null)
        {
            IQueryable<Cart> cartQuery = dbContext.Cart.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && filterQuery != null)
            {
                if (filterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    cartQuery = cartQuery.Where(x => x.UserId == filterQuery.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                if(status == "All")
                {
                    string lowerCaseStatus = status.ToLower();
                    cartQuery = cartQuery.Where(x => x.Status.ToLower() != "Active");
                }
                else
                {
                    string lowerCaseStatus = status.ToLower();
                    cartQuery = cartQuery.Where(x => x.Status.ToLower() == lowerCaseStatus);
                }

            }
            else
            {
                cartQuery = cartQuery.Where(x => x.Status == "Active");
            }

            return await cartQuery.ToListAsync();
        }



        public async Task<Cart> CreateAsync(Cart cart)
        {
            await dbContext.Cart.AddAsync(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
           return await dbContext.Cart.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Cart?> UpdateAsync(Cart cart, Guid id)
        {
           var existingCart=await dbContext.Cart.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingCart == null) {
                return null;
            }
            //existingCart.UserId = cart.UserId;
            existingCart.Status = cart.Status;

            await dbContext.SaveChangesAsync();
            return existingCart;
        }

        public async Task<Cart?> DeleteAsync(Guid id)
        {
            var existingCart = await dbContext.Cart.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCart == null)
            {
                return null;
            }
            dbContext.Cart.Remove(existingCart);
            await dbContext.SaveChangesAsync();
            return existingCart;
        }

    }
}
