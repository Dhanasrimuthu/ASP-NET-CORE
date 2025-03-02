using Microsoft.EntityFrameworkCore;
using storedetail.Data;
using storedetail.model.domain;
using storedetail.Model.Dtos;
using System.Text.RegularExpressions;

namespace storedetail.Repositories
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly storedetailDbContext dbContext;

        public SqlProductRepository(storedetailDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product Product)
        {
            await dbContext.Product.AddAsync(Product);
            await dbContext.SaveChangesAsync();
            return Product;
        }
       public async Task<List<UserProductData[]>> GetUserDataAsync(Guid? userId, string? status)
        {
            string id = "2ED0BCEA-1ECC-4E66-B0E3-D367E6059632";
            var UserProductData =new List<UserProductData>();
            if (status == "All")
            {
                var result = (from product in dbContext.Product
                            join ci in dbContext.CartItem on product.Id equals ci.ProductId
                            join c in dbContext.Cart on ci.CartId equals c.Id
                            join store in dbContext.Store on product.StoreId equals store.Id
                            where !c.Status.Contains("Active") && c.UserId == userId
                             
                              select new UserProductData
                            {
                               Id = product.Id,
                               Name= product.Name,
                               Description =product.Description,
                               Category = product.Category,
                               Image = product.Image,
                               StoreId = product.StoreId,
                               Price = product.Price,
                               Status = c.Status,
                               CartItemId=ci.Id,
                               CartId=c.Id,
                               StoreName = store.Name,
                               CreatedDateFormatted = c.CreatedDate.ToString("yyyy-MM-dd")
                              }).ToList();
                UserProductData = result;

            }
            else
            {
                var result = (from product in dbContext.Product
                              join ci in dbContext.CartItem on product.Id equals ci.ProductId
                              join c in dbContext.Cart on ci.CartId equals c.Id
                              join store in dbContext.Store on product.StoreId equals store.Id
                              where c.Status == (status ?? "Active") && c.UserId == userId
                              
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
                                  StoreName=store.Name,
                                  CreatedDateFormatted = c.CreatedDate.ToString("yyyy-MM-dd")
                              }).ToList();

                UserProductData = result;


            }
            // return UserProductData;
             var groupedResult = UserProductData
            .GroupBy(upd => upd.CartId)
            .Select(g => g.ToArray())
            .ToList();
            return groupedResult;
        }
        public async Task<List<Product>> GetAllAsync(string? filterOn=null, string? filterQuery = null, int pageNumber = 1, int pageSize = 40)
        {
            var product = dbContext.Product.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    product = product.Where(x => x.Category.Contains(filterQuery));
                }
                else if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    product = product.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("StoreId", StringComparison.OrdinalIgnoreCase))
                {
                    if (Guid.TryParse(filterQuery, out Guid filterGuid))
                    {
                        product = product.Where(x => x.StoreId == filterGuid);
                    }
                }
            }

                var SkipResult = (pageNumber - 1) * pageSize;
            return await product.Skip(SkipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var existingProduct = await dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null) 
            { 
                return null;
            }
            dbContext.Product.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
            var existingProduct=await dbContext.Product.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingProduct == null)
            { 
                return null;
            }
            existingProduct.Category = product.Category;
            existingProduct.Description = product.Description;  
            existingProduct.Image=product.Image;
            existingProduct.Name = product.Name;   
            existingProduct.Price=product.Price;    
            existingProduct.StoreId = product.StoreId;

            await dbContext.SaveChangesAsync();
            return existingProduct;
            
        }
    }
}
