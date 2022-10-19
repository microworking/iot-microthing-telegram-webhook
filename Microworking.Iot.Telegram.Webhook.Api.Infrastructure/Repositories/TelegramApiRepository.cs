using System;
using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Application.Infra;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Base;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Interfaces.Repositories;

namespace Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Repositories
{
    public class TelegramApiRepository : HttpRepository<TelegramHttpConfig>, ITelegramApiRepository
    {
        private readonly TelegramHttpConfig _httpConfig;

        public TelegramApiRepository(TelegramHttpConfig httpConfig) : base(httpConfig)
        {
            _httpConfig = httpConfig;
        }

        public async Task<Message> Notify(SendMessageRequest request, IdentityDTO identity)
        {
            try
            {
                string urlBase = _httpConfig.UrlBase.Host;
                _httpConfig.UrlBase = new Uri("https://" + urlBase + "/" + identity.AuthToken);
                base.ConfigureClient(_httpConfig);
                
                request.chat_id = identity.ChatId;

                Message response = await Rest<ITelegramRestApi>().Notify(request);
                
                return (Message)response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}