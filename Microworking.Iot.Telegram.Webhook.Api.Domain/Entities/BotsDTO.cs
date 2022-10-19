using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class BotsDTO
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public string IndetityToken { get; set; }
        public string AuthToken { get; set; }
        public string Owner { get; set; }
        public string BotAlias { get; set; }
        public string BotName { get; set; }
        public DateTime Date { get; set; }
    }
}