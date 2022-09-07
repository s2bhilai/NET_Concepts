using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UnitTesting_Moq.Unit;
using Moq;

namespace Mocking.XUnitTestProject1
{
    public class CreateSomethingTests
    {
        public class StoreMock : IStore
        {
            public int SaveAttempts { get; set; }
            public bool SaveResult { get; set; }
            public Something LastSavedSomething { get; set; }
            public bool Save(Something something)
            {
                SaveAttempts++;
                LastSavedSomething = something;
                return SaveResult;
            }
        }

        public readonly Mock<IStore> _storeMock = new();


        [Fact]
        public void DoesntSavetoDatabaseWhenInvalidSomething()
        {
            //var storemock = new StoreMock();
            //CreateSomething something = new(storemock);
            CreateSomething something = new(_storeMock.Object);

            var createSomethingResult = something.Create(null);

            Assert.False(createSomethingResult.success);
            //Assert.Equal(0, storemock.SaveAttempts);

            _storeMock.Verify(x => x.Save(It.IsAny<Something>()), Times.Never);
        }

        [Fact]
        public void SaveSomethingtoDatabaseWhenValid()
        {
            var something1 = new Something { Name = "Foo" };
            CreateSomething createSomething = new(_storeMock.Object);
            _storeMock.Setup(x => x.Save(something1)).Returns(true);
            
            var createSomethingResult = createSomething.Create(something1);

            Assert.True(createSomethingResult.success);
            _storeMock.Verify(x => x.Save(something1), Times.Once);
        }
    }
}
