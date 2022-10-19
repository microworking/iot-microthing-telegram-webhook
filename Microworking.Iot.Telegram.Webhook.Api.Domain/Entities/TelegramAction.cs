using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class TelegramAction
    {
        public bool ok { get; set; }
        public Update[] result { get; set; }
    }
}