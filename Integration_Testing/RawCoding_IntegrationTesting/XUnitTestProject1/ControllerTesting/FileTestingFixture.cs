using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using RawCoding_IntegrationTesting;
using RawCoding_IntegrationTesting.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitTestProject1.ControllerTesting
{
    public class FileTestingFixture : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        public Stream TestFile { get; private set; }
        private string _cleanupPath;

        public async Task InitializeAsync()
        {
            TestFile = await GetTestImage();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                services.Configure<FileSettings>(fs =>
                {
                    fs.Path = "test_images";
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                _cleanupPath = Path.Combine(env.WebRootPath, "test_images");

            });
        }

        public async Task<Stream> GetTestImage()
        {
            var memoryStream = new MemoryStream();
            var fileStream = File.OpenRead("one.jpg");
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();

            return memoryStream;
        }

        public Task DisposeAsync()
        {
            var directoryInfo = new DirectoryInfo(_cleanupPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            Directory.Delete(_cleanupPath);

            return Task.CompletedTask;
        }

        
    }
}
