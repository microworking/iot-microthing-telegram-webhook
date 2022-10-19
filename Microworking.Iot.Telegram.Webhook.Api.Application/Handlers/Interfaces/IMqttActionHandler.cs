using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IMqttActionHandler
    {
        public Task<string> Handle(SetActionRequest Request, ActionCommandDTO Action, DeviceModelDTO Device);
    }
}