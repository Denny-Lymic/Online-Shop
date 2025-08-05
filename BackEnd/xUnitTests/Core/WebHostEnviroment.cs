using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;


namespace xUnitTests.Core
{
    public class FakeWebHostEnvironment : IAsyncLifetime
    {
        public IWebHostEnvironment Env { get; } = BuildEnv();

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync()
        {
            Directory.Delete(Env.ContentRootPath, recursive: true);
            return Task.CompletedTask;
        }

        private static IWebHostEnvironment BuildEnv()
        {
            var root = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var web = Path.Combine(root, "wwwroot");

            Directory.CreateDirectory(Path.Combine(web, "images", "products"));

            return new CustomWebHostEnvironment
            {
                ContentRootPath = root,
                EnvironmentName = Environments.Development,
                ApplicationName = "UnitTests",
                WebRootPath = web,
                ContentRootFileProvider = new PhysicalFileProvider(root),
                WebRootFileProvider = new PhysicalFileProvider(web)
            };
        }
    }
}
