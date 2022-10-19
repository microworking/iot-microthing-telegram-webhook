
namespace Microworking.Iot.Telegram.Webhook.Api.Application.Infra
{
    public class MqttConfig
    {
        public string Host { set; get; }
        public int Port { set; get; }
        public string ClientId { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int KeepAlive { set; get; }
        public int QoS { set; get; }
        public string Topic { set; get; }
    }
}