using System.Collections.Generic;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IActionHandler
    {
        public ActionPayload ActionCoordinator(SetActionRequest Request, List<string> Terms);
    }
}