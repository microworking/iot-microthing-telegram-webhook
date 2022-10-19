using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}