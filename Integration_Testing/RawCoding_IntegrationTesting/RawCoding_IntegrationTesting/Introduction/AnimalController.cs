using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Introduction
{
    public class AnimalController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public AnimalController(AppDbContext ctx)
        {
            _ctx = ctx;
        }


        [HttpGet]
        public IActionResult List()
        {
            return Ok(_ctx.Animals.ToList());
        }

        public IActionResult Get(int id)
        {
            return Ok(_ctx.Animals.FirstOrDefault(x => x.Id == id));
        }
    }
}
