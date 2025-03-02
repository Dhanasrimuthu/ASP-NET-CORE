using storedetail.model.domain;

namespace storedetail.model.Dtos
{
    public class CartIteamDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        
    }
}
