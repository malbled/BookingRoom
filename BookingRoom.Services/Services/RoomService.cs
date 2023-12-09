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
    /// <inheritdoc cref="IRoomService"/>
    public class RoomService : IRoomService, IServiceAnchor
    {
        private readonly IRoomWriteRepository roomWriteRepository;
        private readonly IRoomRedRepository roomRedRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public RoomService(IRoomWriteRepository roomWriteRepository, IRoomRedRepository roomRedRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.roomWriteRepository = roomWriteRepository;
            this.roomRedRepository = roomRedRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<RoomModel> IRoomService.AddAsync(RoomModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Room>(model);

            roomWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<RoomModel>(item);
        }

        async Task IRoomService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetRoom = await roomRedRepository.GetByIdAsync(id, cancellationToken);

            if (targetRoom == null)
            {
                throw new BookingEntityNotFoundException<Room>(id);
            }

            if (targetRoom.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Номер с идентификатором {id} уже удален");
            }

            roomWriteRepository.Delete(targetRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<RoomModel> IRoomService.EditAsync(RoomModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetRoom = await roomRedRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetRoom == null)
            {
                throw new BookingEntityNotFoundException<Room>(source.Id);
            }

            targetRoom = mapper.Map<Room>(source);

            roomWriteRepository.Update(targetRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<RoomModel>(targetRoom);
        }

        async Task<IEnumerable<RoomModel>> IRoomService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await roomRedRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<RoomModel>(x));
        }

        async Task<RoomModel?> IRoomService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await roomRedRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Room>(id);
            }

            return mapper.Map<RoomModel>(item);
        }
    }
}
