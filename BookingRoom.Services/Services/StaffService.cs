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
    /// <inheritdoc cref="IStaffService"/>
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffWriteRepository staffWriteRepository;
        private readonly IStaffRedRepository staffRedRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;
        public StaffService(IStaffWriteRepository staffWriteRepository, IStaffRedRepository staffRedRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.staffWriteRepository = staffWriteRepository;
            this.staffRedRepository = staffRedRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<StaffModel> IStaffService.AddAsync(StaffModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Staff>(model);

            staffWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StaffModel>(item);
        }

        async Task IStaffService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetStaff = await staffRedRepository.GetByIdAsync(id, cancellationToken);

            if (targetStaff == null)
            {
                throw new BookingEntityNotFoundException<Staff>(id);
            }

            if (targetStaff.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Сотрудник с идентификатором {id} уже удален");
            }

            staffWriteRepository.Delete(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<StaffModel> IStaffService.EditAsync(StaffModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetStaff = await staffRedRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetStaff == null)
            {
                throw new BookingEntityNotFoundException<Staff>(source.Id);
            }

            targetStaff = mapper.Map<Staff>(source);

            staffWriteRepository.Update(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StaffModel>(targetStaff);
        }

        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffRedRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<StaffModel>(x));
        }

        async Task<StaffModel?> IStaffService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffRedRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Staff>(id);
            }

            return mapper.Map<StaffModel>(item);
        }
    }
}
