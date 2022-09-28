using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController:ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ClientPolicy _clientPolicy;

        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory
            clientFactory)
        {
            _clientFactory = clientFactory;
            _clientPolicy = clientPolicy;
        }

        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {            
            //var client = new HttpClient();
            var client = _clientFactory.CreateClient("Test");

            //var response = await client.GetAsync("https://localhost:7212/api/response/25");

            //var response = await _clientPolicy.LinearHttpRetry
            //    .ExecuteAsync(() => client.GetAsync("https://localhost:7212/api/response/25"));

            if(_clientPolicy.circuitBreakerPolicy.CircuitState 
                == Polly.CircuitBreaker.CircuitState.Open)
            {
                throw new Exception("Service currently unavailable");
            }

            var responseCircuit = await _clientPolicy.circuitBreakerPolicy
                .ExecuteAsync(() => 
                client.GetAsync("https://localhost:7212/api/response/25"));


            if(responseCircuit.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Response Service returned SUCCESS");
                return Ok();
            }

            Console.WriteLine("--> Response Service returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
