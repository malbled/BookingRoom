using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IStaffRedRepository"/>
    /// </summary>
    public class StaffReadRepository : IStaffRedRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public StaffReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }
        Task<IReadOnlyCollection<Staff>> IStaffRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Staff>()
                .NotDeletedAt()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.MiddleName)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Staff?> IStaffRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Staff>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Staff>> IStaffRedRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Staff>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.MiddleName)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IStaffRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Staff>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
