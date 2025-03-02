namespace storedetail.Model.Dtos
{
    public class UserProductData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Guid StoreId { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public string StoreName { get; set; }
        public string CreatedDateFormatted { get; set; }
    }
}
