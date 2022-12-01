using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IGetActionHandler
    {
        public Task<ActionPayload> Handle(ActionPayload request);
    }
}