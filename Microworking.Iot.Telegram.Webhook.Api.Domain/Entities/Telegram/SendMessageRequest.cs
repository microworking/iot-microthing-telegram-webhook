
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class SendMessageRequest
    {
        public long? chat_id { get; set; }
        public string? text { get; set; }
        //public string? parse_mode { get; set; }
        //public MessageEntity[]? entities { get; set; }
        //public bool? disable_web_page_preview { get; set; }
        //public bool? disable_notification { get; set; }
        //public bool? protect_content { get; set; }
        //public int? reply_to_message_id { get; set; }
        //public bool? allow_sending_without_reply { get; set; }
        //public dynamic? reply_markup { get; set; }
    }
}