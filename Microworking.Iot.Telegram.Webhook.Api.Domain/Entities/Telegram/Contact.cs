
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class Contact
    {
        public string? phone_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int? user_id { get; set; }
        public string? vcard { get; set; }
    }
}