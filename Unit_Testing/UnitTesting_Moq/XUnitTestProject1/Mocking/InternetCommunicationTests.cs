using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitTesting_Moq.Unit;
using Xunit;

namespace XUnitTestProject1.Mocking
{
    public class InternetCommunicationTests
    {
        public class MockHttpHandler: HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent("[\"foo\",\"bar\"\",\"baz\"]")
                });
            }
        }

        [Fact]
        public async void FetchNamesFetchedNames()
        {
            var client = new HttpClient(new MockHttpHandler())
            {
                BaseAddress = new("http://example.com")
            };

            var iCom = new InternetCommunications(client);

            var names = (await iCom.FetchName()).ToList();

            Assert.Equal(3, names.Count);
            Assert.Contains("foo", names);
            Assert.Contains("bar", names);
            Assert.Contains("baz", names);


        }
    }
}
