
namespace Checkout.ApiServices.SharedModels
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal ShippingCost { get; set; }
        public string TrackingUrl { get; set; }
    }
}