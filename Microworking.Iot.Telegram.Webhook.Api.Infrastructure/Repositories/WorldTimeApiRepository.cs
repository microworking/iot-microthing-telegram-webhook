using System;
using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Application.Infra;
using Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Base;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Repositories
{
    public class WorldTimeApiRepository : HttpRepository<WorldTimeHttpConfig>, IWorldTimeApiRepository
    {
        private readonly WorldTimeHttpConfig _httpConfig;

        public WorldTimeApiRepository(WorldTimeHttpConfig httpConfig) : base(httpConfig)
        {
            _httpConfig = httpConfig;
        }

        public async Task<WorldTimeResponse> GetTime()
        {
            try
            {
                string urlBase = _httpConfig.UrlBase.Host;
                _httpConfig.UrlBase = new Uri("https://" + urlBase + "/api/timezone/america/sao_paulo");
                base.ConfigureClient(_httpConfig);

                WorldTimeResponse response = await Rest<IWorldTimeRestApi>().GetTime();

                return (WorldTimeResponse)response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}