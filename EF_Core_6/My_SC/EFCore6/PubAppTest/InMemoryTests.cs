using Microsoft.EntityFrameworkCore;
using PublisherConsole;
using PublisherData;
using PublisherDomain;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace PubAppTest
{
    public class InMemoryTests
    {
        [Fact]
        public void CanInsertAuthorIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder.UseInMemoryDatabase("CanInsertAuthorIntoDatabase");

            using(var context = new PubContext(builder.Options))
            {
                var author = new Author { FirstName = "a", LastName = "b" };
                context.Authors.Add(author);

                Assert.Equal(EntityState.Added, context.Entry(author).State);
            }            
        }

        [Fact]
        public void InsertAuthorsReturnsCorrectResultNumber()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder.UseInMemoryDatabase("InsertAuthorsReturnsCorrectResultNumber");

            var authorList = new List<ImportAuthorDTO>()
            {
                new ImportAuthorDTO("a","b"),
                new ImportAuthorDTO("c","d"),
                new ImportAuthorDTO("e","f")
            };

            var dl = new DataLogic(new PubContext(builder.Options));
            var result = dl.ImportAuthors(authorList);

            Assert.Equal(authorList.Count, result);
        }
    }
}