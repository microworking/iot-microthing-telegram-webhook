using MediatR;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class Update
    {
        public int? update_id { get; set; }
        public Message? message { get; set; }
        public Message? edited_message { get; set; }
        public Message? channel_post { get; set; }
        public Message? edited_channel_post { get; set; }
        public InlineQuery? inline_query { get; set; }
        public ChosenInlineResult? chosen_inline_result { get; set; }
        public CallbackQuery? callback_query { get; set; }
        public ShippingQuery? shipping_query { get; set; }
        public PreCheckoutQuery? pre_checkout_query { get; set; }
        public Poll? poll { get; set; }
        public PollAnswer? poll_answer { get; set; }
        public ChatMemberUpdated? my_chat_member { get; set; }
        public ChatMemberUpdated? chat_member { get; set; }
        public ChatJoinRequest? chat_join_request { get; set; }
    }
}