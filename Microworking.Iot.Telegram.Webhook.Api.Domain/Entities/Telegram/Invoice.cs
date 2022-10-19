
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class Invoice
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? start_parameter { get; set; }
        public string? currency { get; set; }
        public int? total_amount { get; set; }
    }
}