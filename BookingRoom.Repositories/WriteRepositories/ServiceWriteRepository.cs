using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IServiceWriteRepository"/>
    /// </summary>
    public class ServiceWriteRepository : BaseWriteRepository<Service>, IServiceWriteRepository, IRepositoryAnchor
    {
        public ServiceWriteRepository(IDbWriterContext writerContext)
          : base(writerContext)
        {

        }
    }
}
