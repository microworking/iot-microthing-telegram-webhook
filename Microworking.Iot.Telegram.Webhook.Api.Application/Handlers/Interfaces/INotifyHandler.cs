using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface INotifyHandler
    {
        public Task<IActionResult> Handle(NotifyRequest request, CancellationToken cancellationToken);

        public Task<IActionResult> Handle(SendMessageRequest Request, IdentityDTO Identity);
    }
}