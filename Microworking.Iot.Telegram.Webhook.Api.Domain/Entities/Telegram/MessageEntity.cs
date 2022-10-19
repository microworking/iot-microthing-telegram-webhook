
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class MessageEntity
    {
        public string? type { get; set; }
        public int? offset { get; set; }
        public int? length { get; set; }
        public string? url { get; set; }
        public User? user { get; set; }
        public string? language { get; set; }
        public string? custom_emoji_id { get; set; }
    }
}