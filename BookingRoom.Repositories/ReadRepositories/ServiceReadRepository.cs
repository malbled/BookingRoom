using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IServiceRedRepository"/>
    /// </summary>
    public class ServiceReadRepository : IServiceRedRepository, IRepositoryAnchor
    {

        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public ServiceReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Service>> IServiceRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Service>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Service?> IServiceRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Service>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Service>> IServiceRedRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Service>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IServiceRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Service>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
