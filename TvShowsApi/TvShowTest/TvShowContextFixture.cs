using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace TvShowTest
{
    public class TvShowContextFixture : IDisposable
    {
        private bool _disposed;
        public Context Context => FakeContext();

        private Context FakeContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
               //.UseInMemoryDatabase(Guid.NewGuid().ToString())
               .EnableSensitiveDataLogging()
               .Options;

            return new Context(options);
        }

        ~TvShowContextFixture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (_disposed) return;

            if (dispose)
            {
                Context?.Dispose();
            }

            _disposed = true;
        }
    }
}
