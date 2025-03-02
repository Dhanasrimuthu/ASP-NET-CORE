namespace storedetail.model.domain
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
