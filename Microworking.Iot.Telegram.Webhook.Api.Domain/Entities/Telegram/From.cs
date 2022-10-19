
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class From
    {
        public long? id { get; set; }
        public bool? is_bot { get; set; }
        public string? first_name { get; set; }
        public string? username { get; set; }
    }
}