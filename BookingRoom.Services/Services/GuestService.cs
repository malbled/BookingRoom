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
    /// <inheritdoc cref="IGuestService"/>
    public class GuestService : IGuestService, IServiceAnchor
    {
        private readonly IGuestWriteRepository guestWriteRepository;
        private readonly IGuestRedRepository guestRedRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public GuestService(
            IGuestWriteRepository guestWriteRepository, 
            IGuestRedRepository guestRedRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceValidatorService validatorService)
        {
            this.guestRedRepository = guestRedRepository;
            this.guestWriteRepository = guestWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<GuestModel> IGuestService.AddAsync(GuestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Guest>(model);

            guestWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<GuestModel>(item);
        }

        async Task IGuestService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetGuest = await guestRedRepository.GetByIdAsync(id, cancellationToken);
            if (targetGuest == null)
            {
                throw new BookingEntityNotFoundException<Guest>(id);
            }

            if (targetGuest.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Гость с идентификатором {id} уже удален");
            }

            guestWriteRepository.Delete(targetGuest);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<GuestModel> IGuestService.EditAsync(GuestModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetGuest = await guestRedRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetGuest == null)
            {
                throw new BookingEntityNotFoundException<Guest>(source.Id);
            }

            targetGuest = mapper.Map<Guest>(source);

            guestWriteRepository.Update(targetGuest);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<GuestModel>(targetGuest);
        }

        async Task<IEnumerable<GuestModel>> IGuestService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await guestRedRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<GuestModel>(x));
        }

        async Task<GuestModel?> IGuestService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await guestRedRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Guest>(id);
            }

            return mapper.Map<GuestModel>(item);
        }
    }
}
