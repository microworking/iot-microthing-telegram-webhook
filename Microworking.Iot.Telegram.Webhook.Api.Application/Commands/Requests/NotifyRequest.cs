using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests
{
    public class NotifyRequest : IRequest<IActionResult>
    {
        public NotifyRequest() { }

        public NotifyRequest(SendMessageRequest request)
        {
            this.Request = request;
        }

        public string IndetityToken { get; set; }
        //public string? Uid { get; set; }
        public SendMessageRequest Request { get; set; }
    }
}