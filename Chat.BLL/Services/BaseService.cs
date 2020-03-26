namespace Chat.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chat.BLL.Infrastructure;
    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;
    using Chat.DTO;

    public abstract class BaseService<DomainEntity, DTO> : IBaseService<DomainEntity, DTO>
        where DomainEntity : BaseEntity, new()
        where DTO : BaseEntityDTO, new()
    {
        protected readonly IUnitOfWork unitOfWork;

        protected readonly IRepository<DomainEntity> currentRepository;

        public BaseService(IUnitOfWork unitOfWork, IRepository<DomainEntity> currentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.currentRepository = currentRepository;
        }

        public virtual DTO Add(DTO entity)
        {
            var entityToAdd = DTOService.ToEntity<DTO, DomainEntity>(entity);
            entityToAdd.CreatedOn = DateTime.Now;
            this.currentRepository.Insert(entityToAdd);
            this.unitOfWork.Commit();
            return DTOService.ToDTO<DomainEntity, DTO>(entityToAdd);
        }

        public virtual bool Delete(int id)
        {
            bool deleteResult;
            var entityToDelete = this.currentRepository.GetById(id);
            if (entityToDelete == null)
            {
                deleteResult = false;
            }
            else
            {
                this.currentRepository.Delete(id);
                this.unitOfWork.Commit();
                deleteResult = true;
            }

            return deleteResult;
        }

        public virtual DTO Get(int id)
        {
            var entity = this.currentRepository.GetById(id);
            return DTOService.ToDTO<DomainEntity, DTO>(entity);
        }

        public virtual IEnumerable<DTO> Get()
        {
            var entities = this.currentRepository.Get();
            return entities.Select(DTOService.ToDTO<DomainEntity, DTO>);
        }

        public virtual DTO Update(DTO entity)
        {
            var changedDomainEntity = DTOService.ToEntity<DTO, DomainEntity>(entity);
            changedDomainEntity.LastModified = DateTime.Now;
            this.currentRepository.Update(changedDomainEntity);
            this.unitOfWork.Commit();
            return DTOService.ToDTO<DomainEntity, DTO>(changedDomainEntity);
        }
    }
}
