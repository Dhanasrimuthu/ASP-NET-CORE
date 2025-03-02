namespace storedetail.model.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string image { get; set; }
        public Guid StoreId { get; set; }
        public string Price { get; set; }
    }
}
