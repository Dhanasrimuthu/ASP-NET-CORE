namespace storedetail.model.Dtos
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid userId { get; set; }
    }
}
