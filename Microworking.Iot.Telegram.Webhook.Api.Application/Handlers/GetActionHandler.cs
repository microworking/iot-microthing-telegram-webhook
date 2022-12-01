//TODO: para criar tokens
//var token = string.Join(null, Guid.NewGuid().ToString().Replace("-", "").Concat(new Random().Next(100000000, 1000000000).ToString()));
//fl token = "8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE";
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Services;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class GetActionHandler : IGetActionHandler
    {
        //private readonly IMqttActionsService _mqttActionsService;
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly INotifyHandler _notifyHandler;
        private readonly ILogger<GetActionHandler> _logger;

        public GetActionHandler(//IMqttActionsService mqttActionsService, 
                                ITelegramDbRepository telegramDbRepository,
                                IAuthTokenHandler authTokenHandler,
                                INotifyHandler notifyHandler,
                                ILogger<GetActionHandler> logger)
        {
            //_mqttActionsService = mqttActionsService;
            _telegramDbRepository = telegramDbRepository;
            _authTokenHandler = authTokenHandler;
            _notifyHandler = notifyHandler;
            _logger = logger;
        }

        public async Task<ActionPayload> Handle(ActionPayload action)
        {
            if (action != null && action.message != null)
            {
                if(action.message.Contains(action.uid))
                {
                    DeviceModelDTO device = _telegramDbRepository.GetPeripheral(action.uid);

                    action.message = action.message.Replace(action.uid, device.Name);
                }

                if (action.identity_token == "8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE" && action.action == "broadcast")
                {
                    IEnumerable<IdentityDTO> identities = _authTokenHandler.GetIdentity(action.uid, action.identity_token);
                    if (identities is null) return new ActionPayload();
                    _ = _notifyHandler.Handle(action.message, identities.ToList());

                }
                else if (action.identity_token == "8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE" && action.action == "ping")
                {
                    ActionPayload payload = new ActionPayload()
                    {
                        identity_token = action.identity_token,
                        uid = action.uid,
                        owner = "Microworking.Iot.Telegram.Webhook.Api",
                        action = "echo",
                        gpio = 0,
                        message = "Datetime refresh.",                        
                        peripheral = action.uid,
                        update_id = 0,
                        chat_id = 0,
                        message_id = 0,
                        date = DateTime.UtcNow.AddDays(-3)
                    };
                    return payload;
                }
                else
                {
                    IdentityDTO identity = _authTokenHandler.GetIdentity(action.identity_token);
                    if (!identity.IsAuthorized) return new ActionPayload();
                    SendMessageRequest message = new SendMessageRequest();
                    message.chat_id = identity.ChatId;
                    message.text = action.message;
                    _ = await _notifyHandler.Handle(message, identity);
                }
                //TODO: quando implementar "reactions" a chamada será feita aqui
                //List<string> terms = MessageHelper.DivideTerms(request.reaction);
                //ActionPayload action = _actionHandler.ActionCoordinator(request, terms);
            }

            return new ActionPayload();
        }
    }
}