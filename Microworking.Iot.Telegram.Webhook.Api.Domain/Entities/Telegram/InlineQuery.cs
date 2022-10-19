
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class InlineQuery
    {
        public string? id { get; set; }
        public User? from { get; set; }
        public string? query { get; set; }
        public string? offset { get; set; }
        public string? chat_type { get; set; }
        public Location? location { get; set; }
    }
}