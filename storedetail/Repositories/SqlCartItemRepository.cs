using Microsoft.EntityFrameworkCore;
using storedetail.Data;
using storedetail.model.domain;
using storedetail.Model.Dtos;
using System.Security.Claims;

namespace storedetail.Repositories
{
    public class SqlCartItemRepository : ICartItemRepository
    {
        private readonly storedetailDbContext dbContext;

        public SqlCartItemRepository(storedetailDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CartItem> CreateAsync(CartItem cartItem)
        {
            await dbContext.CartItem.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();
            return cartItem;
        }


        public async Task<bool> CartItemExistsAsync(Guid cartId, Guid productId)
        {
            return await dbContext.CartItem.AnyAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<List<CartItem>> GetAllAsync(string? filterOn = null, Guid? filterQuery = null, string? status = null)
        {
            IQueryable<Cart> cartQuery = dbContext.Cart.AsQueryable();

            if (string.IsNullOrEmpty(status) )
            {
                return await dbContext.CartItem.ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(filterOn) && filterQuery != null)
            {
                if (filterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    cartQuery = cartQuery.Where(x => x.UserId == filterQuery.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "All")
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
            var cartIds = await cartQuery.Select(c => c.Id).ToListAsync();
            var cartItems = await dbContext.CartItem.Where(ci => cartIds.Contains(ci.CartId)).ToListAsync();
            return cartItems;
        }

       
        public async Task<List<UserProductData>> GetByIdAsync(Guid CartId)
        {
            var UserProductData = new List<UserProductData>();
            var result = (from product in dbContext.Product
                          join ci in dbContext.CartItem on product.Id equals ci.ProductId
                          join c in dbContext.Cart on ci.CartId equals c.Id
                          join store in dbContext.Store on product.StoreId equals store.Id
                          where ci.CartId == CartId

                          select new UserProductData
                          {
                              Id = product.Id,
                              Name = product.Name,
                              Description = product.Description,
                              Category = product.Category,
                              Image = product.Image,
                              StoreId = product.StoreId,
                              Price = product.Price,
                              Status = c.Status,
                              CartItemId = ci.Id,
                              CartId = c.Id,
                              StoreName = store.Name,
                              CreatedDateFormatted = c.CreatedDate.ToString("yyyy-MM-dd")
                          }).ToList();

            UserProductData = result;

               return UserProductData;
    }
    /*
            public async Task<CartItem?> UpdateAsync(Guid id, CartItem cartItem)
            {
                var existingItem =await dbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
                if (existingItem == null)
                {
                    return null;
                }
                existingItem.CartId = cartItem.CartId;
                existingItem.ProductId = cartItem.ProductId;
                await dbContext.SaveChangesAsync();
                return existingItem;

            }*/

    public async Task<CartItem?> UpdateAsync(Guid id, string status, Guid userId)
        {
            var existingItem = await dbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
            if (existingItem == null)
            {
                return null;
            }
            var existingCart = await dbContext.Cart.FirstOrDefaultAsync(x => x.UserId == userId && x.Status == status);

            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = status,
                };

                await dbContext.Cart.AddAsync(newCart);
                await dbContext.SaveChangesAsync();
                existingItem.CartId = newCart.Id;
            }
            else
            {
                existingItem.CartId = existingCart.Id;
            }

            await dbContext.SaveChangesAsync();

            return existingItem;
        }

        public async Task<CartItem?> DeleteAsync(Guid id)
        {
            var existingItem = await dbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
            if (existingItem == null)
            {
                return null;
            }
            dbContext.CartItem.Remove(existingItem);
            await dbContext.SaveChangesAsync();
            return existingItem;
        }
    }
}
