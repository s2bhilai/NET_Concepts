using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Controllers
{
    [ApiController]
    [Route("/api/animals")]
    public class AnimalController: ControllerBase
    {
        public IActionResult GetAnimals([FromServices] IAnimalService service)
        {
            return Ok(service.GetAnimal());
        }
    }
}
