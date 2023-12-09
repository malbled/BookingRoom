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
    /// <inheritdoc cref="IHotelService"/>
    public class HotelService : IHotelService, IServiceAnchor
    {
        private readonly IHotelRedRepository hotelRedRepositiry;
        private readonly IHotelWriteRepository hotelWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public HotelService(IHotelRedRepository hotelRedRepositiry, IMapper mapper,
            IHotelWriteRepository hotelWriteRepository, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.hotelRedRepositiry = hotelRedRepositiry;
            this.mapper = mapper;
            this.hotelWriteRepository = hotelWriteRepository;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<HotelModel> IHotelService.AddAsync(HotelModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Hotel>(model);
            hotelWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<HotelModel>(item);
        }

        async Task IHotelService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetHotel = await hotelRedRepositiry.GetByIdAsync(id, cancellationToken);

            if (targetHotel == null)
            {
                throw new BookingEntityNotFoundException<Hotel>(id);
            }

            if (targetHotel.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Отель с идентификатором {id} уже удален");
            }

            hotelWriteRepository.Delete(targetHotel);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<HotelModel> IHotelService.EditAsync(HotelModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetHotel = await hotelRedRepositiry.GetByIdAsync(source.Id, cancellationToken);

            if (targetHotel == null)
            {
                throw new BookingEntityNotFoundException<Hotel>(source.Id);
            }

            targetHotel = mapper.Map<Hotel>(source);
            hotelWriteRepository.Update(targetHotel);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<HotelModel>(targetHotel);
        }

        async Task<IEnumerable<HotelModel>> IHotelService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await hotelRedRepositiry.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<HotelModel>(x));
        }

        async Task<HotelModel?> IHotelService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await hotelRedRepositiry.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Hotel>(id);
            }

            return mapper.Map<HotelModel>(item);
        }
    }
}
