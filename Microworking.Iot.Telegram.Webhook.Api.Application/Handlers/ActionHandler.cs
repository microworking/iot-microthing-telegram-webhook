using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Enums;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Commands.Requests;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class ActionHandler : IActionHandler
    {
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly IMqttActionHandler _mqttActionHandler;
        private readonly IDatabaseActionHandler _databaseActionHandler;
        private readonly IHttpActionHandler _httpActionHandler;
        private readonly INotifyHandler _notifyHandler;
        private readonly ILogger<GetActionHandler> _logger;

        public ActionHandler(IAuthTokenHandler authTokenHandler,
                             ITelegramDbRepository telegramDbRepository,
                             IMqttActionHandler mqttActionHandler,
                             IDatabaseActionHandler databaseActionHandler,
                             IHttpActionHandler httpActionHandler,
                             INotifyHandler notifyHandler,
                             ILogger<GetActionHandler> logger)
        {
            _authTokenHandler = authTokenHandler;
            _telegramDbRepository = telegramDbRepository;
            _mqttActionHandler = mqttActionHandler;
            _databaseActionHandler = databaseActionHandler;
            _httpActionHandler = httpActionHandler;
            _notifyHandler = notifyHandler;
            _logger = logger;
        }

        public ActionPayload ActionCoordinator(SetActionRequest Request, List<string> Terms)
        {
            SendMessageRequest message = new SendMessageRequest();
            IdentityDTO identity = Request.Identity;
            message.chat_id = identity.ChatId;

            ActionCommandDTO action = this.FindAction(Terms);
            string room = this.FindRoom(identity.IndentyToken, Terms);
            switch (action.ActionTypeId)
            {
                case (int)ActionTypeEnum.MqttAction:
                    if (!string.IsNullOrEmpty(room))
                    {
                        DeviceModelDTO device = this.FindDevice(identity.IndentyToken, room, Terms);
                        if (device != null)
                            message.text = _mqttActionHandler.Handle(Request, action, device).Result;
                        else
                        {
                            message.text = $"O periférico informado para o ambiente { room } não foi localizado!";
                        }
                    }
                    else
                    {
                        //TODO: implementar para ligar todos perifericos
                        message.text = $"Informe o ambiente e um periférico válido para sua ação!";
                    }
                    break;

                case (int)ActionTypeEnum.DatabseAction:
                    //_databaseActionHandler.Handle(action, Identity);

                    //templates txt
                    break;

                //case (int)ActionTypeEnum.ActionNotFound:
                //    if (!string.IsNullOrEmpty(room))
                //    {
                //        DeviceModelDTO device = this.FindDevice(identity.IndentyToken, room, Terms);
                //        if (device != null)
                //            message.text = _mqttActionHandler.Handle(Request, action, device).Result;
                //        else
                //        {
                //            message.text = $"O periférico informado para o ambiente { room } não foi localizado!";
                //        }
                //    }
                //    else
                //    {
                //        //TODO: implementar para ligar todos perifericos
                //        message.text = $"Informe o ambiente e um periférico válido para sua ação!";
                //    }
                //    break;
                //    //_mqttTestActionHandler.ToString();

                    //beep - teste mqtt
                    //dados técnicos do mcu
                    break;

                case (int)ActionTypeEnum.HttpAction:
                    _httpActionHandler.Handle(action, identity, Terms);
                    break;

                case (int)ActionTypeEnum.ActionNotFound:
                    message.text = "Ação não reconhecida :-(";
                    break;
            }

            _notifyHandler.Handle(message, identity);

            return null;
        }

        private ActionCommandDTO FindAction(List<string> Terms)
        {
            return _telegramDbRepository.GetActionCommand(Terms);
        }

        private string FindRoom(string IndetityToken, List<string> Terms)
        {
            return _telegramDbRepository.GetRoom(IndetityToken, Terms);
        }

        private DeviceModelDTO FindDevice(string IndentityToken, string Room, List<string> Terms)
        {
            return _telegramDbRepository.GetDeviceProperties(IndentityToken, Room, Terms);
        }
    }
}