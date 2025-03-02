namespace storedetail.Model.Dtos
{
    public class AddCartItemDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
