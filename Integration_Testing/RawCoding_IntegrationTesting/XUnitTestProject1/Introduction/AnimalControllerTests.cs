using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RawCoding_IntegrationTesting.Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Introduction
{
    public class AnimalControllerTests
    {
        [Fact]
        public void AnimalController_ListsAnimalsFromDatabase()
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            using(AppDbContext ctx = new(optionsBuilder.Options))
            {
                ctx.Add(new Animal { Name = "Foo", Type = "Bar" });
                ctx.SaveChanges();
            }

            IActionResult result;
            using(AppDbContext ctx = new(optionsBuilder.Options))
            {
                result = new AnimalController(ctx).List();
            }

            var okResult = Assert.IsType<OkObjectResult>(result);
            var animals = Assert.IsType<List<Animal>>(okResult.Value);
            var animal = Assert.Single(animals);
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }

        [Fact]
        public void AnimalController_GetsAnimalsFromDatabase()
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            using (AppDbContext ctx = new(optionsBuilder.Options))
            {
                ctx.Add(new Animal { Name = "Foo", Type = "Bar" });
                ctx.SaveChanges();
            }

            IActionResult result;
            using (AppDbContext ctx = new(optionsBuilder.Options))
            {
                result = new AnimalController(ctx).Get(1);
            }

            var okResult = Assert.IsType<OkObjectResult>(result);
            var animal = Assert.IsType<Animal>(okResult.Value);
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }
    }
}
