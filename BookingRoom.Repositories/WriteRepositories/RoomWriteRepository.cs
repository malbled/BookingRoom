using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IRoomWriteRepository"/>
    /// </summary>
    public class RoomWriteRepository : BaseWriteRepository<Room>, IRoomWriteRepository, IRepositoryAnchor
    {
        public RoomWriteRepository(IDbWriterContext writerContext)
         : base(writerContext)
        {

        }
    }
}
