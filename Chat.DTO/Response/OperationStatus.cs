namespace Chat.DTO.Response
{
    using System.Runtime.Serialization;

    [DataContract]
    public enum OperationStatus
    {
        [EnumMember]
        Success,

        [EnumMember]
        Fail
    }
}