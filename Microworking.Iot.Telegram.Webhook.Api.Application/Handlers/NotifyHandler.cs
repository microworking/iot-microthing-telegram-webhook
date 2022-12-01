using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;
using System.Collections.Generic;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class NotifyHandler : INotifyHandler, IRequestHandler<NotifyRequest, IActionResult>
    {
        private readonly ITelegramApiRepository _telegramApiRepository;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly ILogger<GetActionHandler> _logger;

        public NotifyHandler(ITelegramApiRepository telegramApiRepository,
                             ITelegramDbRepository telegramDbRepository,
                             ILogger<GetActionHandler> logger)
        {
            _telegramApiRepository = telegramApiRepository;
            _telegramDbRepository = telegramDbRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(NotifyRequest request, CancellationToken cancellationToken)
        {
            IdentityDTO identity = _telegramDbRepository.GetIdentity(request.IndetityToken);

            return (IActionResult)await _telegramApiRepository.Notify(request.Request, identity);
        }

        public async Task<IActionResult> Handle(SendMessageRequest Request, IdentityDTO Identity)
        {
            return (IActionResult)await _telegramApiRepository.Notify(Request, Identity);
        }

        public async Task<IActionResult> Handle(string Message, List<IdentityDTO> Identities)
        {
            foreach (IdentityDTO identity in Identities)
            {
                if (identity.IsAuthorized)
                {
                    SendMessageRequest message = new SendMessageRequest();
                    message.chat_id = identity.ChatId;
                    message.text = Message;
                    _telegramApiRepository.Notify(message, identity);
                }
            }
            return null;
        }
    }
}
/**
Outros membros da entidade:
    request.allow_sending_without_reply = true;
    request.disable_notification = false;
    request.disable_web_page_preview = true;
    request.entities = null;
    request.parse_mode = null;
    request.protect_content = null;
    request.reply_markup = null;
    request.reply_to_message_id = null;
*/