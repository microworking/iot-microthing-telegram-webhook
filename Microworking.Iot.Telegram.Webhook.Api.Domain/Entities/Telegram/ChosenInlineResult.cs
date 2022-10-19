
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class ChosenInlineResult
    {
        public string? result_id { get; set; }
        public User? from { get; set; }
        public Location? location { get; set; }
        public string? inline_message_id { get; set; }
        public string? query { get; set; }
    }
}