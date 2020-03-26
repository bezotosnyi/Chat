namespace Chat.BLL.Infrastructure
{
    using System.Collections.Generic;

    using Chat.Domain.Entities;
    using Chat.DTO;

    public interface IBaseService<DomainEntity, DTO>
        where DomainEntity : BaseEntity, new() where DTO : BaseEntityDTO, new()
    {
        DTO Add(DTO entity);

        bool Delete(int id);

        DTO Get(int id);

        IEnumerable<DTO> Get();

        DTO Update(DTO entity);
    }
}