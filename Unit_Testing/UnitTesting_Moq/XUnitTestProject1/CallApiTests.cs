using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class CallApiTests
    {

        public class HttpMessageHandlerMock : HttpMessageHandler
        {
            private HttpStatusCode _code;
            private HttpResponseMessage? _response;

            public HttpMessageHandlerMock(HttpStatusCode code)
            {
                _code = code;
            }

            public HttpMessageHandlerMock(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> 
                SendAsync(HttpRequestMessage request, 
                CancellationToken cancellationToken)
            {

                if(_response != null)
                {
                    return Task.FromResult(_response);
                }

                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = _code
                });
            }
        }


        [Fact]
        public async Task ReturnNull_When_400()
        {
            var http = new HttpClient(
                new HttpMessageHandlerMock(HttpStatusCode.BadRequest));

            var service = new Service(http);
            var result = await service.Create();

            Assert.Null(result);

        }

        [Fact]
        public async Task ReturnJson_When_200()
        {
            var http = new HttpClient(
                new HttpMessageHandlerMock(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"foo\": 42}")
            }));

            var service = new Service(http);
            var result = await service.Create();

            Assert.NotNull(result);
            Assert.Equal("{\"foo\": 42}", result);

        }
    }

    public class Service
    {
        private HttpClient _http;

        public Service(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<string> Create()
        {
            var response = await _http.GetAsync("http://www.google.com");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
