
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public class Message
    {
        public int? message_id { get; set; }
        public From? from { get; set; }
        public Chat? sender_chat { get; set; }
        public int? date { get; set; }
        public Chat? chat { get; set; }
        public User? forward_from { get; set; }
        public Chat? forward_from_chat { get; set; }
        public int? forward_from_message_id { get; set; }
        public string? forward_signature { get; set; }
        public string? forward_sender_name { get; set; }
        public int? forward_date { get; set; }
        public bool? is_automatic_forward { get; set; }
        public Message? reply_to_message { get; set; }
        public User? via_bot { get; set; }
        public int? edit_date { get; set; }
        public bool? has_protected_content { get; set; }
        public string? media_group_id { get; set; }
        public string? author_signature { get; set; }
        public string? text { get; set; }
        public MessageEntity[]? entities { get; set; }
        public Animation? animation { get; set; }
        public Audio? audio { get; set; }
        public Document? document { get; set; }
        public PhotoSize[]? photo { get; set; }
        public Sticker? sticker { get; set; }
        public Video? video { get; set; }
        public VideoNote? video_note { get; set; }
        public Voice? voice { get; set; }
        public string? caption { get; set; }
        public MessageEntity[]? caption_entities { get; set; }
        public Contact? contact { get; set; }
        public Dice? dice { get; set; }
        public Game? game { get; set; }
        public Poll? poll { get; set; }
        public Venue? venue { get; set; }
        public Location? location { get; set; }
        public User[]? new_chat_members { get; set; }
        public User? left_chat_member { get; set; }
        public string? new_chat_title { get; set; }
        public PhotoSize[]? new_chat_photo { get; set; }
        public bool? delete_chat_photo { get; set; }
        public bool? group_chat_created { get; set; }
        public bool? supergroup_chat_created { get; set; }
        public bool? channel_chat_created { get; set; }
        public MessageAutoDeleteTimerChanged? message_auto_delete_timer_changed { get; set; }
        public int? migrate_to_chat_id { get; set; }
        public int? migrate_from_chat_id { get; set; }
        public Message? pinned_message { get; set; }
        public Invoice? invoice { get; set; }
        public SuccessfulPayment? successful_payment { get; set; }
        public string? connected_website { get; set; }
        public PassportData? passport_data { get; set; }
        public ProximityAlertTriggered? proximity_alert_triggered { get; set; }
        public VideoChatScheduled? video_chat_scheduled { get; set; }
        public VideoChatStarted? video_chat_started { get; set; }
        public VideoChatEnded? video_chat_ended { get; set; }
        public VideoChatParticipantsInvited? video_chat_participants_invited { get; set; }
        public WebAppData? web_app_data { get; set; }
        public InlineKeyboardMarkup? reply_markup { get; set; }
    }
}