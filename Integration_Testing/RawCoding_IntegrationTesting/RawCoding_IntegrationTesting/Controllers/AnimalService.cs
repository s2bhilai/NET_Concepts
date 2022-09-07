using RawCoding_IntegrationTesting.Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Controllers
{
    public class AnimalService : IAnimalService
    {
        public Animal GetAnimal()
        {
            return new()
            {
                Id = 1,
                Name = "Foo",
                Type = "Bar"
            };
        }
    }
}
