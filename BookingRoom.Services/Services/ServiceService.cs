using AutoMapper;
using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;
using BookingRoom.Services.Anchors;
using BookingRoom.Services.Contracts.Exceptions;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ServicesContracts;
using BookingRoom.Services.Validator;

namespace BookingRoom.Services.Services
{
    /// <inheritdoc cref="IServiceService"/>
    public class ServiceService : IServiceService, IServiceAnchor
    {
        private readonly IServiceWriteRepository serviceWriteRepository;
        private readonly IServiceRedRepository serviceRedRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;
        public ServiceService(IServiceWriteRepository serviceWriteRepository, IServiceRedRepository serviceRedRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.serviceWriteRepository = serviceWriteRepository;
            this.serviceRedRepository = serviceRedRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<ServiceModel> IServiceService.AddAsync(ServiceModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Service>(model);

            serviceWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceModel>(item);
        }

        async Task IServiceService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetService = await serviceRedRepository.GetByIdAsync(id, cancellationToken);

            if (targetService == null)
            {
                throw new BookingEntityNotFoundException<Service>(id);
            }

            if (targetService.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Услуга с идентификатором {id} уже удалена");
            }

            serviceWriteRepository.Delete(targetService);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        async Task<ServiceModel> IServiceService.EditAsync(ServiceModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetService = await serviceRedRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetService == null)
            {
                throw new BookingEntityNotFoundException<Service>(source.Id);
            }

            targetService = mapper.Map<Service>(source);

            serviceWriteRepository.Update(targetService);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ServiceModel>(targetService);
        }

        async Task<IEnumerable<ServiceModel>> IServiceService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await serviceRedRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<ServiceModel>(x));
        }

        async Task<ServiceModel?> IServiceService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await serviceRedRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Service>(id);
            }

            return mapper.Map<ServiceModel>(item);
        }
    }
}
