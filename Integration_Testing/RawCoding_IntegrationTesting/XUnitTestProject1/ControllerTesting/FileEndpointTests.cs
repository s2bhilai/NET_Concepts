using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RawCoding_IntegrationTesting;
using RawCoding_IntegrationTesting.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.ControllerTesting
{
    public class FileEndpointTests:IClassFixture<FileTestingFixture>
    {
        private FileTestingFixture _factory;

        public FileEndpointTests(FileTestingFixture webApplicationFactory)
        {
            _factory = webApplicationFactory;
        }

        [Fact]
        public async void SaveFileToDisk()
        {
            var client = _factory.CreateClient();

            MultipartFormDataContent form = new();

            form.Add(new StreamContent(_factory.TestFile), "file", "one.jpg");
            var response = await client.PostAsync("/api/files", form);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var fileResponse = await client.GetAsync("/test_images/one.jpg");
            Assert.Equal(HttpStatusCode.OK, fileResponse.StatusCode);
        }



       


    }
}
