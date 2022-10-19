
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public interface IChatMemberMember
    {
        public string? status { get; set; }
        public User? user { get; set; }
    }
}