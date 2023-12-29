using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IRoomRedRepository"/>
    /// </summary>
    public class RoomReadRepository : IRoomRedRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public RoomReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Room>> IRoomRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Room>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ThenBy(x => x.TypeRoom)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Room?> IRoomRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Room>()
            .NotDeletedAt()
                .ById(id)

                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Room>> IRoomRedRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Room>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Title)
                .ThenBy(x => x.TypeRoom)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IRoomRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Room>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
