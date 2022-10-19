using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class HomeDTO
    {
        public int Id { get; set; }
        public int Cep { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}