using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class IdentityDTO
    {
        public bool IsAuthorized { get; set; }
        public string IndentyToken { get; set; }
        public string AuthToken { get; set; }
        public long ChatId { get; set; }
        public string BotAlias { get; set; }
        public string BotName { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public int Cep { get; set; }
        public DateTime Date { get; set; }
    }
}