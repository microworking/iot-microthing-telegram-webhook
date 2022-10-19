using System;
using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Application.Infra;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Base;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Repositories
{
    public class ViaCepApiRepository : HttpRepository<ViaCepHttpConfig>, IViaCepApiRepository
    {
        private readonly ViaCepHttpConfig _httpConfig;

        public ViaCepApiRepository(ViaCepHttpConfig httpConfig) : base(httpConfig)
        {
            _httpConfig = httpConfig;
        }

        public async Task<ViaCepResponse> GetLocation(IdentityDTO Identity)
        {
            try
            {
                string urlBase = _httpConfig.UrlBase.Host;
                _httpConfig.UrlBase = new Uri("https://" + urlBase + "/ws/" + Identity.Cep.ToString("D8") + "/json"); ;
                base.ConfigureClient(_httpConfig);

                ViaCepResponse response = await Rest<IViaCepRestApi>().GetLocation();

                return (ViaCepResponse)response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}