
namespace Microworking.Iot.Telegram.Webhook.Api.Application.Services
{
    public class ExternalService
    {
        private readonly IMqttActionsService mqttClientService;

        public ExternalService(MqttActionsServiceProvider provider)
        {
            mqttClientService = provider.MqttClientService;
        }
    }
}