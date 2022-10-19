using System;
using System.Threading.Tasks;
using Microworking.Iot.Telegram.Webhook.Api.Application.Infra;
using Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Base;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;
using Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses;

namespace Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Repositories
{
    public class OpenWeatherApiRepository : HttpRepository<OpenWeatherHttpConfig>, IOpenWeatherApiRepository
    {
        private readonly OpenWeatherHttpConfig _httpConfig;

        public OpenWeatherApiRepository(OpenWeatherHttpConfig httpConfig) : base(httpConfig)
        {
            _httpConfig = httpConfig;
        }

        public async Task<OpenWeatherResponse> GetWeather()
        {
            try
            {
                string urlBase = _httpConfig.UrlBase.Host;
                _httpConfig.UrlBase = new Uri("https://" + urlBase + "/data/2.5/weather?q=S%C3%A3o%20Paulo&appid=97005c9f24a8026bcbe3ad72e56aa0c9");
                base.ConfigureClient(_httpConfig);

                OpenWeatherResponse response = await Rest<IOpenWeatherRestApi>().GetWeather();

                return (OpenWeatherResponse)response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}