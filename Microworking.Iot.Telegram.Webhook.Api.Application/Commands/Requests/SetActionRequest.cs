using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests
{
    public class SetActionRequest : Update, IRequest<IDeviceResult<SetActionResponse>> 
    {
        public IdentityDTO Identity { get; set;  }
    }
}