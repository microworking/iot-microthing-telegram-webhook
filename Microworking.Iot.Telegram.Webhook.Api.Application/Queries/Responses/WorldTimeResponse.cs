﻿
namespace Microworking.Iot.Telegram.Webhook.Api.Application.Queries.Responses
{
    public class WorldTimeResponse
    {
        public string? abbreviation { get; set; }
        public string? client_ip { get; set; }
        public string? datetime { get; set; }
        public int? day_of_week { get; set; }
        public int? day_of_year { get; set; }
        public bool? dst { get; set; }
        public string? dst_from { get; set; }
        public int? dst_offset { get; set; }
        public string? dst_until { get; set; }
        public int? raw_offset { get; set; }
        public string? timezone { get; set; }
        public long? unixtime { get; set; }
        public string? utc_datetime { get; set; }
        public string? utc_offset { get; set; }
        public int? week_number { get; set; }
    }
}