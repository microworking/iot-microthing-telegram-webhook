
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class ActionCommandDTO
    {
        public string ActionName { get; set; }
        public string ActionCommand { get; set; }
        public int ActionTypeId { get; set; }
        public string ActionTypeName { get; set; }
    }
}