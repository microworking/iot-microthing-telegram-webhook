using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using MQTTnet.Client.Receiving;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IGetActionHandler
    {
        public Task<bool> Handle(ActionPayload request);
    }
}