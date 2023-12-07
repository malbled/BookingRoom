using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IGuestWriteRepository"/>
    /// </summary>
    public class GuestWriteRepository : BaseWriteRepository<Guest>, IGuestWriteRepository, IRepositoryAnchor
    {
        public GuestWriteRepository(IDbWriterContext writerContext)
           : base(writerContext)
        {

        }
    }
}
