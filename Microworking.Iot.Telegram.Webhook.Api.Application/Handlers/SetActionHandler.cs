using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MediatR;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Application.Helpers;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class SetActionHandler : ISetActionHandler, IRequestHandler<SetActionRequest, IDeviceResult<SetActionResponse>>
    {
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly IActionHandler _actionHandler;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly ILogger<GetActionHandler> _logger;

        public SetActionHandler(IAuthTokenHandler authTokenHandler,
                                IActionHandler actionHandler,
                                ITelegramDbRepository telegramDbRepository,
                                IMqttActionsService mqttActionsService,
                                ILogger<GetActionHandler> logger)
        {
            _authTokenHandler = authTokenHandler;
            _actionHandler = actionHandler;
            _telegramDbRepository = telegramDbRepository;
            _logger = logger;
        }

        public async Task<IDeviceResult<SetActionResponse>> Handle(SetActionRequest request, CancellationToken cancellationToken)
        {
            if (request != null && request.message != null && !string.IsNullOrEmpty(request.message.text))
            {
                List<string> terms = MessageHelper.DivideTerms(request.message.text);
                
                ActionPayload action = _actionHandler.ActionCoordinator(request, terms);

                return new DeviceResult<SetActionResponse>(true);
            }
            else
                return new DeviceResult<SetActionResponse>(false, "no actions pool");
        }
    }
}

//    StringBuilder builder = new StringBuilder();
//    builder.Append($"Boa noite, bem vindo ao bot ");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Bot);
//    builder.Append("\r\n\r\n");
//    builder.Append("Modelo: ");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Name);
//    builder.Append("\r\n");
//    builder.Append("Dispositivo: ");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Device);
//    builder.Append("\r\n");
//    builder.Append("Proprietário:");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Owner);
//    builder.Append("\r\n");
//    builder.Append("Localização:");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Address);
//    builder.Append("\r\n");
//    builder.Append("Data de registro:");
//    //builder.Append(_telegramIotDTO?.FirstOrDefault()?.Date);
//    builder.Append("\r\n\r\n");
//    builder.Append("*** Comandos ***");
//    builder.Append("\r\n");
//    builder.Append("info - Informações do dispositivo");
//    builder.Append("\r\n");
//    builder.Append("status - Estado atual do dispositivo");
//    builder.Append("\r\n");
//    builder.Append("start - Inicia um controle");
//    builder.Append("\r\n");
//    builder.Append("stop - Para um controle");
//    builder.Append("\r\n");
//    builder.Append("restart - Reinicia o dispositivo");
//    builder.Append("\r\n");
//    builder.Append("config - Configura o dispositivo (não implementado)");
//    builder.Append("\r\n\r\n");
//    builder.Append("Para mais informações e suporte escreva para microworkingsistemas@gmail.com");
