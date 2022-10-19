
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class CallbackGame
    {
        public int? user_id { get; set; }
        public int? score { get; set; }
        public bool? force { get; set; }
        public bool? disable_edit_message { get; set; }
        public int? chat_id { get; set; }
        public int? message_id { get; set; }
        public string? inline_message_id { get; set; }
    }
}