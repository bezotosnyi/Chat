namespace Chat.Domain.Entities
{
    using System;

    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? LastModified { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}