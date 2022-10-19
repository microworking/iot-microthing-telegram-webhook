using System.Threading;
using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Responses;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface ISetActionHandler
    {
        public Task<IDeviceResult<SetActionResponse>> Handle(SetActionRequest request, CancellationToken cancellationToken);
    }
}