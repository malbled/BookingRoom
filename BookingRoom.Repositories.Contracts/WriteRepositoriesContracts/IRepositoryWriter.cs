﻿using BookingRoom.Common.Entity.EntityInterface;
using System.Diagnostics.CodeAnalysis;

namespace BookingRoom.Repositories.Contracts.WriteRepositoriesContracts
{
    /// <summary>
    /// Интерфейс для работы с БД
    /// </summary>
    /// <typeparam name="TEntity"> Сущность из БД</typeparam>
    public interface IRepositoryWriter<in TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
