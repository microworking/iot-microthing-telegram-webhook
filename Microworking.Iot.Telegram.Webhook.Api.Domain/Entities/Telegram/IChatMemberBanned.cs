
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public interface  IChatMemberBanned
    {
        public string? status { get; set; }
        public User? user { get; set; }
        public int? until_date { get; set; }
    }
}