using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IHotelWriteRepository"/>
    /// </summary>
    public class HotelWriteRepository : BaseWriteRepository<Hotel>, IHotelWriteRepository, IRepositoryAnchor
    {
        public HotelWriteRepository(IDbWriterContext writerContext)
          : base(writerContext)
        {

        }
    }
}
