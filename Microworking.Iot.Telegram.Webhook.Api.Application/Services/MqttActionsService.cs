using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Disconnecting;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Services
{
    public class MqttActionsService : IMqttActionsService
    {
        private readonly IGetActionHandler _getActionHandler;
        private readonly ILogger<MqttActionsService> _logger;
        private IMqttClient _mqttClient;
        private IMqttClientOptions _options;

        public MqttActionsService(IGetActionHandler getActionHandler,
                                  IMqttClientOptions options,
                                  ILogger<MqttActionsService> logger)
        {
            _getActionHandler = getActionHandler;
            _logger = logger;
            _options = options;
            _mqttClient = new MqttFactory().CreateMqttClient();
            
            _mqttClient.ConnectedHandler = this;
            _mqttClient.DisconnectedHandler = this;
            _mqttClient.ApplicationMessageReceivedHandler = this;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //System.Console.WriteLine("start");
            await _mqttClient.ConnectAsync(_options);
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ReconnectAsync();
            }
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            //System.Console.WriteLine("connected");
            await _mqttClient.SubscribeAsync("microworking/telegram-iot/microthingv1/cloud");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //System.Console.WriteLine("stop");
            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    ReasonCode = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDiconnection"
                };
                await _mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await _mqttClient.DisconnectAsync();
        }

        public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            System.Console.WriteLine("disconnected");
            return null;
        }

        public async Task<bool> PublishAsync(ActionPayload payload)
        {
            MqttApplicationMessage mqttApplicationMessage = new MqttApplicationMessage()
            {
                Topic = "microworking/telegram-iot/microthingv1/broker",
                ContentType = "text/json",
                Payload = Encoding.Default.GetBytes(JsonSerializer.Serialize(payload))
            };
            
            if (!_mqttClient.IsConnected) await StartAsync(new CancellationToken());

            MqttClientPublishResult result = await _mqttClient.PublishAsync(mqttApplicationMessage);

            return true;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            Console.WriteLine("received: " + Encoding.Default.GetString(eventArgs.ApplicationMessage.Payload));

            ActionPayload action = JsonSerializer.Deserialize<ActionPayload>(eventArgs.ApplicationMessage.Payload);

            _getActionHandler.Handle(action);

            return null;
        }
    }
}