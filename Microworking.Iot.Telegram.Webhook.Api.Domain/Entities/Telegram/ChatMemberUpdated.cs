
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class ChatMemberUpdated
    {
        public Chat? chat { get; set; }
        public User? from { get; set; }
        public int? date { get; set; }
        public ChatMember? old_chat_member { get; set; }
        public ChatMember? new_chat_member { get; set; }
        public ChatInviteLink? invite_link { get; set; }
    }
}