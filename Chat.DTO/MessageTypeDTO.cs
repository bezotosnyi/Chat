namespace Chat.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public enum MessageTypeDTO
    {
        [EnumMember]
        Private,

        [EnumMember]
        Public
    }
}