using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class MqttActionHandler : IMqttActionHandler
    {
        private readonly IMqttActionsService _mqttActionsService;
        private readonly ILogger<GetActionHandler> _logger;

        public MqttActionHandler(IMqttActionsService mqttActionsService,
                                 ILogger<GetActionHandler> logger)
        {
            _mqttActionsService = mqttActionsService;
            _logger = logger;
        }

        public async Task<string> Handle(SetActionRequest Request, ActionCommandDTO Action, DeviceModelDTO Device)
        {
            ActionPayload payload = new ActionPayload()
            {
                identity_token = Request.Identity.IndentyToken,
                uid = Device.Uid,
                gpio = Device.Gpio,
                action = Action.ActionCommand,
                update_id = Request.update_id,
                chat_id = Request.Identity.ChatId,
                message_id = Request.message.message_id,
                peripheral = Device.Name,
                date = DateTime.UtcNow.AddDays(-3)
            };

            bool result = await _mqttActionsService.PublishAsync(payload);

            if(result)
                return $"Iniciando o atendimento da solicitação { Action.ActionName.ToLower() } para o periférico { Device.Name.ToLower() }";
            else
                return $"Ocorreu um erro e o atendimento da solicitação { Action.ActionName.ToLower() } para o periférico { Device.Name.ToLower() } não pode ser iniciada";
        }
    }
}