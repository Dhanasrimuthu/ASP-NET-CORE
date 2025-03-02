using Microsoft.EntityFrameworkCore;

namespace storedetail.model.domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; } 
        public Guid StoreId { get; set; }
        public string Price {  get; set; }

        
    }
}
