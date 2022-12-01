using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers
{
    public class AuthTokenHandler : IAuthTokenHandler
    {
        private readonly ITelegramDbRepository _telegramDbRepository;
        private readonly ILogger<GetActionHandler> _logger;

        public AuthTokenHandler(ITelegramDbRepository telegramDbRepository,
                                ILogger<GetActionHandler> logger)
        {
            _telegramDbRepository = telegramDbRepository;
            _logger = logger;
        }

        public IdentityDTO GetIdentity(HttpRequest Request)
        {
            StringValues secretToken = string.Empty;

            _ = Request.Headers.TryGetValue("X-Telegram-Bot-Api-Secret-Token", out secretToken);

            if (!string.IsNullOrEmpty(secretToken))
            {
                return GetIdentity(secretToken);
            }

            return new IdentityDTO { IsAuthorized = false };
        }

        public IdentityDTO GetIdentity(string IndetityToken)
        {
            if (!string.IsNullOrEmpty(IndetityToken))
            {
                return _telegramDbRepository.GetIdentity(IndetityToken);
            }
            else if(IndetityToken == "8o1nx9zfTFUiAkYdgWR3GUlX9fxpREFOQw1dtoidR0jfa5ihR0alIj9GmuV4YrIE")
            {
                return _telegramDbRepository.GetIdentity(IndetityToken);
            }

            return new IdentityDTO { IsAuthorized = false };
        }

        public IEnumerable<IdentityDTO> GetIdentity(string Uid, string IndetityToken) 
        {
            if (!string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(IndetityToken))
            {
                return _telegramDbRepository.GetIdentity(Uid, IndetityToken);
            }

            return new IdentityDTO[] { }.AsEnumerable<IdentityDTO>();
        }
    }
}