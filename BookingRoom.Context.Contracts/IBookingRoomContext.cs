using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Context.Contracts
{
    public interface IBookingRoomContext
    {
        /// <summary>Список <inheritdoc cref="Hotel"/></summary>
        DbSet<Hotel> Hotels { get; }

        /// <summary>Список <inheritdoc cref="Guest"/></summary>
        DbSet<Guest> Guests { get; }

        /// <summary>Список <inheritdoc cref="Service"/></summary>
        DbSet<Service> Services { get; }

        /// <summary>Список <inheritdoc cref="Room"/></summary>
        DbSet<Room> Rooms { get; }

        /// <summary>Список <inheritdoc cref="Staff"/></summary>
        DbSet<Staff> Staffs { get; }

        /// <summary>Список <inheritdoc cref="Booking"/></summary>
        DbSet<Booking> Bookings { get; }
    }
}
