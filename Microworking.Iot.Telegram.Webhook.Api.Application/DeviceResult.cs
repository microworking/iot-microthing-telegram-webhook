
namespace Microworking.Iot.Telegram.Webhook.Api.Application
{
    public class DeviceResult<T> : IDeviceResult<T>
    {
        public bool? sucess { get; set; }
        public string? info { get; set; }
        public T message { get; set; }

        public DeviceResult() {}

        public DeviceResult(bool? sucess)
        {
            this.sucess = sucess;
        }

        public DeviceResult(bool? sucess, string? info)
        {
            this.sucess = sucess;
            this.info = info;
        }

        public DeviceResult(bool? sucess, T message)
        {
            this.sucess = sucess;
            this.message = message;
        }

        public DeviceResult(bool? sucess, string? info, T message)
        {
            this.sucess = sucess;
            this.info = info;
            this.message = message;
        }
    }
}