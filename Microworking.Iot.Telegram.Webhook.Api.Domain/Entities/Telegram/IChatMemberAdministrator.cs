
namespace Microworking.Iot.Telegram.Webhook.Api.Domain.Entities.Telegram
{
    public interface IChatMemberAdministrator
    {
        public string? status { get; set; }
        public User? user { get; set; }
        public bool? can_be_edited { get; set; }
        public bool? is_anonymous { get; set; }
        public bool? can_manage_chat { get; set; }
        public bool? can_delete_messages { get; set; }
        public bool? can_manage_video_chats { get; set; }
        public bool? can_restrict_members { get; set; }
        public bool? can_promote_members { get; set; }
        public bool? can_change_info { get; set; }
        public bool? can_invite_users { get; set; }
        public bool? can_post_messages { get; set; }
        public bool? can_edit_messages { get; set; }
        public bool? can_pin_messages { get; set; }
        public string? custom_title { get; set; }
    }
}