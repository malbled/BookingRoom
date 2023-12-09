using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;

namespace BookingRoom.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IStaffWriteRepository"/>
    /// </summary>
    public class StaffWriteRepository : BaseWriteRepository<Staff>, IStaffWriteRepository, IRepositoryAnchor
    {
        public StaffWriteRepository(IDbWriterContext writerContext)
          : base(writerContext)
        {

        }
    }
}
