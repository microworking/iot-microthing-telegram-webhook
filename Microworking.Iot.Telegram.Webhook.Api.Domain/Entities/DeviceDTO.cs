using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public int RoomId { get; set; }
        public string Uid { get; set; }
        public DateTime Date { get; set; }
    }
}