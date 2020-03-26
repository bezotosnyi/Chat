namespace Chat.DTO.Response
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /*[KnownType(typeof(string))]
    [KnownType(typeof(UserDTO))]
    [KnownType(typeof(List<MessageDTO>))]
    [KnownType(typeof(List<UserDTO>))]*/
    [DataContract]
    public class OperationStatusInfo<T>
    {
        public OperationStatusInfo()
        {
        }

        public OperationStatusInfo(OperationStatus operationStatus, string attachedInfo)
        {
            this.OperationStatus = operationStatus;
            this.AttachedInfo = attachedInfo;
        }

        public OperationStatusInfo(OperationStatus operationStatus, string attachedInfo, T attachedObject)
        {
            this.OperationStatus = operationStatus;
            this.AttachedInfo = attachedInfo;
            this.AttachedObject = attachedObject;
        }

        [DataMember]
        public OperationStatus OperationStatus { get; set; }

        [DataMember]
        public string AttachedInfo { get; set; }

        [DataMember]
        public T AttachedObject { get; set; }
    }
}