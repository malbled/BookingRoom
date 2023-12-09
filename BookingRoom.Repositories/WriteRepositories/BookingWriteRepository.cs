using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IBookingWriteRepository"/>
    /// </summary>
    public class BookingWriteRepository : BaseWriteRepository<Booking>, IBookingWriteRepository, IRepositoryAnchor
    {
        public BookingWriteRepository(IDbWriterContext writerContext)
           : base(writerContext)
        {

        }
    }
}
