namespace storedetail.model.domain
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; } 

        public Product Product { get; set; }
    }
}
