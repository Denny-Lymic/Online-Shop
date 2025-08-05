using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;


namespace xUnitTests.Data
{
    public abstract class InMemoryDb : IAsyncLifetime
    {
        private SqliteConnection _connection = null!;
        protected DbContextOptions<ShopDbContext> _options = null!;

        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            await _connection.OpenAsync();

            _options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseSqlite(_connection)
                .Options;

            await using var ctx = new ShopDbContext(_options);
            await ctx.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }

        protected ShopDbContext CreateContext() => new ShopDbContext(_options);
    }
}
