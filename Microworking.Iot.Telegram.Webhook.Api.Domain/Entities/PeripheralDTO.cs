using System;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class PeripheralDTO
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int Gpio { get; set; }
        public string Type { get; set; }
        public bool IsAnalogic { get; set; }
        public DateTime Date { get; set; }
    }
}