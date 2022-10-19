
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class PollAnswer
    {
        public string? poll_id { get; set; }
        public User? user { get; set; }
        public int[]? option_ids { get; set; }
    }
}