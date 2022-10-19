using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Requests
{
    public class GetActionsRequest : IRequest<IDeviceResult<GetActionsResponse>>
    {
        public string uid { get; set; }
    }
}