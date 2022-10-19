using System;
using MQTTnet.Client.Options;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Services
{
    public class AspCoreMqttClientOptionBuilder : MqttClientOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspCoreMqttClientOptionBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}