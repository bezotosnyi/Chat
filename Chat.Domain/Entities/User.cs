namespace Chat.Domain.Entities
{
    using System;

    using Chat.Domain.Entities.Enum;

    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? DateOfBirthday { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }
}