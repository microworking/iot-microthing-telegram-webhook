
namespace Microworking.Iot.Telegram.Webhook.Api.Application
{
    public interface IDeviceResult<T>
    {
        public bool? sucess { get; set; }
        public string? info { get; set; }
        public T message { get; set; }
    }
}