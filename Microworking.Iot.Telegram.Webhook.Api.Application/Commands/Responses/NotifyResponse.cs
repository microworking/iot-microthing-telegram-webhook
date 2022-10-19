using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Responses
{
    public class NotifyResponse : TelegramAction, IRequest<IResultado> { }
}