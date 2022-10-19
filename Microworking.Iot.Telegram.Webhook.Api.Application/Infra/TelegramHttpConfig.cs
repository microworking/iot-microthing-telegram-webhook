using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Infra
{
    public class TelegramHttpConfig
    {
        public Uri UrlBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool ValidarCertificadoSsl { get; set; }
    }
}