using Microsoft.AspNetCore.Http;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Handlers.Interfaces
{
    public interface IAuthTokenHandler
    {
        public IdentityDTO GetIdentity(HttpRequest Request);

        public IdentityDTO GetIdentity(string IndetityToken);
    }
}