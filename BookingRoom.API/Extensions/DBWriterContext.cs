﻿using BookingRoom.Common.Entity;
using BookingRoom.Common.Entity.InterfaceDB;

namespace BookingRoom.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDbWriterContext"/>
    /// </summary>
    public class DBWriterContext : IDbWriterContext
    {
        /// <inheritdoc/>
        public IDbWriter Writer { get; }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; }

        /// <inheritdoc/>
        public IDateTimeProvider DateTimeProvider { get; }

        /// <inheritdoc/>
        public string UserName { get; } = "BookingRoom.Api";

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DBWriterContext"/>
        /// </summary>
        /// <remarks>В реальном проекте надо добавлять IIdentity для доступа к
        /// информации об авторизации</remarks>
        public DBWriterContext(
            IDbWriter writer,
            IUnitOfWork unitOfWork, IDateTimeProvider provider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = provider;
        }
    }
}
