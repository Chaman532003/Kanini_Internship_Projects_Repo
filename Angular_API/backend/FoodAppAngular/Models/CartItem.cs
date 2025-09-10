namespace FoodAppAngular.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public Food? Food { get; set; }
    }
}
