using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting_Moq.Unit
{
    public class InternetCommunications
    {
        private HttpClient _client;

        public InternetCommunications(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public Task<IEnumerable<string>> FetchName()
        {
            return _client.GetFromJsonAsync<IEnumerable<string>>("api/names");
        }
    }
}
