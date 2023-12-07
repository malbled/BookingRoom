using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IGuestRedRepository"/>
    /// </summary>
    public class GuestReadRepository : IGuestRedRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public GuestReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Guest>> IGuestRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Guest>()
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.MiddleName)
                .ThenBy(x => x.Passport)
                .ThenBy(x => x.AddressRegistration)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Guest?> IGuestRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Guest>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Guest>> IGuestRedRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Guest>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.MiddleName)
                .ThenBy(x => x.Passport)
                .ThenBy(x => x.AddressRegistration)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IGuestRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Guest>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
