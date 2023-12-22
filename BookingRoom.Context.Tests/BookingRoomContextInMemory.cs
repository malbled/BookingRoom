using BookingRoom.Common.Entity.InterfaceDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

namespace BookingRoom.Context.Tests
{
    public abstract class BookingRoomContextInMemory : IAsyncDisposable
    {
        protected readonly CancellationToken CancellationToken;
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Контекст <see cref="BookingRoomContext"/>
        /// </summary>
        protected BookingRoomContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork"/>
        protected IUnitOfWork UnitOfWork => Context;

        /// <inheritdoc cref="IDbRead"/>
        protected IDbRead Reader => Context;

        protected IDbWriterContext WriterContext => new TestWriterContext(Context, UnitOfWork);

        protected BookingRoomContextInMemory()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken = cancellationTokenSource.Token;
            var optionsBuilder = new DbContextOptionsBuilder<BookingRoomContext>()
                .UseInMemoryDatabase($"MoneronTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new BookingRoomContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            try
            {
                Context.Database.EnsureDeletedAsync().Wait();
                Context.Dispose();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        async public ValueTask DisposeAsync()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            try
            {
                await Context.Database.EnsureDeletedAsync();
                await Context.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
