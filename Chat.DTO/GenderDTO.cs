namespace Chat.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public enum GenderDTO
    {
        [EnumMember]
        [Display(Name = "Мужской")]
        Male,

        [EnumMember]
        [Display(Name = "Женский")]
        Female
    }
}