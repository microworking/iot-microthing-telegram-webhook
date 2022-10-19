//TODO: para criar tokens
//var token = string.Join(null, Guid.NewGuid().ToString().Replace("-", "").Concat(new Random().Next(100000000, 1000000000).ToString()));
//flespi token = "8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE";
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class GetActionHandler : IGetActionHandler
    {
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly INotifyHandler _notifyHandler;
        private readonly ILogger<GetActionHandler> _logger;

        public GetActionHandler(IAuthTokenHandler authTokenHandler,
                                INotifyHandler notifyHandler,
                                ILogger<GetActionHandler> logger)
        {
            _authTokenHandler = authTokenHandler;
            _notifyHandler = notifyHandler;
            _logger = logger;
        }

        public async Task<bool> Handle(ActionPayload action)
        {

            if (action != null && action.message != null)
            {
                IdentityDTO identity = _authTokenHandler.GetIdentity(action.identity_token);
                if (!identity.IsAuthorized) return false;

                //TODO: quando implementar "reactions" a chamada será feita aqui
                //List<string> terms = MessageHelper.DivideTerms(request.reaction);
                //ActionPayload action = _actionHandler.ActionCoordinator(request, terms);

                SendMessageRequest message = new SendMessageRequest();
                message.chat_id = identity.ChatId;
                message.text = action.message;
                _ = await _notifyHandler.Handle(message, identity);

                return true;
            }
            else
                return false;
        }
    }
}