
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public interface  IChatMemberOwner
    {
        public string? status { get; set; }
        public User? user { get; set; }
        public bool? is_anonymous { get; set; }
        public string? custom_title { get; set; }
    }
}