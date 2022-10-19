using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class DeviceActionHandler : IDeviceActionHandler
    {
        private readonly IMqttActionsService _mqttActionsService;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly ILogger<GetActionHandler> _logger;

        public DeviceActionHandler(IMqttActionsService mqttActionsService,
                                 ITelegramDbRepository telegramDbRepository,
                                 ILogger<GetActionHandler> logger)
        {
            _mqttActionsService = mqttActionsService;
            _telegramDbRepository = telegramDbRepository;
            _logger = logger;
        }

        public async Task Handle(SetActionRequest Request, ActionCommandDTO Action, DeviceModelDTO Device)
        {
            ActionPayload payload = new ActionPayload()
            {
                identity_token = Request.Identity.IndentyToken,
                update_id = Request.update_id,
                message_id = Request.message.message_id,
                uid = Device.Uid,
                gpio = Device.Gpio,
                action = Action.ActionCommand,
                date = DateTime.UtcNow.AddDays(-3)
            };
           
            //switch(Action.ActionCommand)
            //{
            //    case "BEEP":

            //        break;

            //}

            await _mqttActionsService.PublishAsync(payload);
        }

    }
}