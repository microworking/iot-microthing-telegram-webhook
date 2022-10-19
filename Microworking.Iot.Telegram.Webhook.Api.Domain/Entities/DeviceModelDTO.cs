using System;
using System.Collections.Generic;
using System.Text;

namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities
{
    public class DeviceModelDTO
    {
        public string Name { get; set; }
        public string Uid { get; set; }
        public int Gpio { get; set; }
        public string Type { get; set; }
        public bool IsAnalogic { get; set; }
    }
}
