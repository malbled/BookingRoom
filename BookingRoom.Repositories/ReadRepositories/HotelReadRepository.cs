using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IHotelRedRepository"/>
    /// </summary>
    public class HotelReadRepository : IHotelRedRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private IDbRead reader;

        public HotelReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Hotel>> IHotelRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Hotel>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Hotel?> IHotelRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hotel>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Hotel>> IHotelRedRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Hotel>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IHotelRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hotel>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
