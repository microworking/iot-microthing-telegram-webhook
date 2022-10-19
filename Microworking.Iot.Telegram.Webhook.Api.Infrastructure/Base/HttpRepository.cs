using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Microworking.Iot.Telegram.Webhook.Api.Infrastructure.Base
{
    public abstract class HttpRepository<T1>
    {
        T1 _httpConfig;

        protected HttpRepository(T1 httpConfig)
        {
            _httpConfig = httpConfig;

            ConfigureClient();
        }

        protected HttpClientHandler HandlerService { get; private set; }

        protected HttpClient ClientService { get; private set; }

        protected T2 Rest<T2>()
        {
            var authHeader = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{_httpConfig.GetType().GetProperty("User").GetValue(_httpConfig)?.ToString()}:{_httpConfig.GetType().GetProperty("Password").GetValue(_httpConfig)?.ToString()}"));

            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(authHeader)
            };
            return RestService.For<T2>(ClientService, refitSettings);
        }

        private HttpClientHandler CriarClientHandler()
        {
            if ((bool)_httpConfig.GetType().GetProperty("ValidarCertificadoSsl").GetValue(_httpConfig))
                HandlerService = new HttpClientHandler();
            else
                HandlerService = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (mst, crt, chn, plc) => true };

            return HandlerService;
        }

        public void ConfigureClient()
            => ClientService = new HttpClient(CriarClientHandler())
            {
                BaseAddress = new Uri(_httpConfig.GetType().GetProperty("UrlBase").GetValue(_httpConfig).ToString())
            };

        public void ConfigureClient(T1 httpConfig)
        {
            _httpConfig = httpConfig;

            ConfigureClient();
        }
    }
}