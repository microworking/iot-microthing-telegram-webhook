using System.Threading.Tasks;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using Microsoft.Extensions.Hosting;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Services
{
    public interface IMqttActionsService : IHostedService,
                                           IMqttClientConnectedHandler,
                                           IMqttClientDisconnectedHandler,
                                           IMqttApplicationMessageReceivedHandler
    {
        Task<bool> PublishAsync(ActionPayload payload);
    }
}