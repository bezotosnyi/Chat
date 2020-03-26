namespace Chat.DTO
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class MessageDTO : BaseEntityDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public UserDTO UserFrom { get; set; }

        [DataMember]
        public UserDTO UserTo { get; set; }

        [DataMember]
        public MessageTypeDTO MessageType { get; set; }

        [DataMember]
        public string MessageContent { get; set; }

        [DataMember]
        public DateTime? CreatedOn { get; set; }

        public override string ToString()
        {
            return $"From '{this.UserFrom}' To '{this.UserTo}' message '{this.MessageContent}'";
        }
    }
}
