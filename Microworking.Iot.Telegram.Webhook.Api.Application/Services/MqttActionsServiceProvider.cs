
namespace Microworking.Iot.Telegram.Webhook.Api.Application.Services
{
    public class MqttActionsServiceProvider
    {
        public readonly IMqttActionsService MqttClientService;

        public MqttActionsServiceProvider(IMqttActionsService mqttClientService)
        {
            MqttClientService = mqttClientService;
        }
    }
}