
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class MaskPosition
    {
        public string? point { get; set; }
        public float? x_shift { get; set; }
        public float? y_shift { get; set; }
        public float? scale { get; set; }
    }
}