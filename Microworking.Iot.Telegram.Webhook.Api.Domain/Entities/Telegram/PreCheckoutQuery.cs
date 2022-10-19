
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class PreCheckoutQuery
    {
        public string? id { get; set; }
        public User? from { get; set; }
        public string? currency { get; set; }
        public int? total_amount { get; set; }
        public string? invoice_payload { get; set; }
        public string? shipping_option_id { get; set; }
        public OrderInfo? order_info { get; set; }
    }
}
