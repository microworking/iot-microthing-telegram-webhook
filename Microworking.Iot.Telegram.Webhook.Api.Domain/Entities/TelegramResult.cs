using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class TelegramResult
    {
        public long message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public long date { get; set; }
        public string text { get; set; }
    }
}