
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class ChatJoinRequest
    {
        public Chat? chat { get; set; }
        public User? from { get; set; }
        public int? date { get; set; }
        public string? bio { get; set; }
        public ChatInviteLink? invite_link { get; set; }
    }
}