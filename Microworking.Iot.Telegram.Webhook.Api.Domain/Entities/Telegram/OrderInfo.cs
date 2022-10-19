
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class OrderInfo
    {
        public string? name { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public ShippingAddress? shipping_address { get; set; }
    }
}