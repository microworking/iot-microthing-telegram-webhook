
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public interface IChatMemberLeft
    {
        public string? status { get; set; }
        public User? user { get; set; }
    }
}