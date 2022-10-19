using System.IO;
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
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class TemplateHander //: ITemplateHander
    {
        private readonly ITelegramApiRepository _telegramApiRepository;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly ILogger<GetActionHandler> _logger;

        public TemplateHander(ITelegramApiRepository telegramApiRepository,
                             ITelegramDbRepository telegramDbRepository,
                             ILogger<GetActionHandler> logger)
        {
            _telegramApiRepository = telegramApiRepository;
            _telegramDbRepository = telegramDbRepository;
            _logger = logger;
        }

        public async Task<string> Handle(ViaCepResponse data)
        {
            //System.IO.File.ReadAllText(@"");



            return null;
            //return (string)await _telegramApiRepository.Notify(request.Request, identity);
        }
    }
}