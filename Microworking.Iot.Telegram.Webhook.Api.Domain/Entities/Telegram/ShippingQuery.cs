
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class ShippingQuery
    {
        public string? id { get; set; }
        public User? from { get; set; }
        public string? invoice_payload { get; set; }
        public ShippingAddress? shipping_address { get; set; }
    }
}