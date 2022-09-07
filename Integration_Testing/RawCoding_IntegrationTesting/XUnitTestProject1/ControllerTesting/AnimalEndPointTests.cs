using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RawCoding_IntegrationTesting;
using RawCoding_IntegrationTesting.Controllers;
using RawCoding_IntegrationTesting.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.ControllerTesting
{
    public class AnimalEndPointTests:IClassFixture<WebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _factory;

        public AnimalEndPointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IAnimalService, AnimalServiceMock>();

                    //var animalService = services.Single(x => x.ServiceType == typeof(IAnimalService));
                    //services.Remove(animalService);
                });
            });
        }

        public class AnimalServiceMock : IAnimalService
        {
            RawCoding_IntegrationTesting.Introduction.Animal 
                IAnimalService.GetAnimal()
            {
                return new()
                {
                    Id = 2,
                    Name = "Foo2",
                    Type = "Bar2"
                };
            }
        }

        [Fact]
        public async Task GetAnimals()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/animals");
            var animal = await response.Content.ReadFromJsonAsync<Animal>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);

            
        }

    }
}
