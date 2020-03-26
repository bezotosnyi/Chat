namespace Chat.Domain.Entities
{
    using Chat.Domain.Entities.Enum;

    public class Message : BaseEntity
    {
        public int UserFromId { get; set; }

        public User UserFrom { get; set; }

        public int UserToId { get; set; }

        public User UserTo { get; set; }

        public MessageType MessageType { get; set; }

        public string MessageContent { get; set; }
    }
}