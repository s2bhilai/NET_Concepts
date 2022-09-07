using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting_Moq.Unit;
using Xunit;

namespace XUnitTestProject1.Mocking
{
    public class DontTestMicrosoftApiTests
    {
        public readonly Mock<IFiles> _fileMock = new();
        
        [Fact]
        public async Task WriteToFileStream()
        {
            var memoryStream = new MemoryStream();
            _fileMock.Setup(x => x.OpenWriteStreamTo("path")).Returns(memoryStream);

            var service = new DontTestMicrosoftApi(_fileMock.Object);

            await service.SaveFile("path", new MemoryStream(new byte[] { 2, 3, 4, 5 }));

            Assert.Equal(4, memoryStream.Length);
        }
    }
}
