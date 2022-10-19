
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class PassportData
    {
        public EncryptedPassportElement[]? data { get; set; }
        public EncryptedCredentials? credentials { get; set; }
    }
}