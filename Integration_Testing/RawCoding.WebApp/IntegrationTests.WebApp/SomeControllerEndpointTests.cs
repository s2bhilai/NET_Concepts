using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.WebApp
{
    public class SomeControllerEndpointTests: IClassFixture<AppInstance>
    {
        private AppInstance _instance;

        public SomeControllerEndpointTests(AppInstance appInstance)
        {
            _instance = appInstance;
        }

        [Fact]
        public async Task PublicTest()
        {
            var client = _instance.CreateClient();
            var result = await client.GetAsync("/public");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Public", content);
        }

        [Fact]
        public async Task Secure()
        {
            var client = _instance
                .AuthenticatedInstance()
                .CreateClient(new()
            {
                AllowAutoRedirect = false
            });
            var result = await client.GetAsync("/secure");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Secure", content);
        }

        [Fact]
        public async Task PrivilegedGod()
        {
            var client = _instance
                .AuthenticatedInstance(new System.Security.Claims.Claim("CustomRoleType","God"))
                .CreateClient(new()
                {
                    AllowAutoRedirect = false
                });
            var result = await client.GetAsync("/privileged");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Privileged", content);
        }

        [Fact]
        public async Task PrivilegedAngel()
        {
            var client = _instance
                .AuthenticatedInstance(new System.Security.Claims.Claim("CustomRoleType", "Angel"))
                .CreateClient(new()
                {
                    AllowAutoRedirect = false
                });
            var result = await client.GetAsync("/privileged");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Privileged", content);
        }
    }
}
