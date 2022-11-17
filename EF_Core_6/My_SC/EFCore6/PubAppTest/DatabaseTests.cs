using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;
using Xunit;

namespace PubAppTest
{
    public class DatabaseTests
    {
        [Fact]
        public void CanInsertAuthorIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();
            builder
              .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog=PubTestDatabase");

            using(var context = new PubContext(builder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var author = new Author { FirstName = "a", LastName = "b" };
                context.Authors.Add(author);

                Debug.WriteLine($"Before Save: {author.AuthorId}");
                context.SaveChanges();
                Debug.WriteLine($"After Save: {author.AuthorId}");

                Assert.NotEqual(0, author.AuthorId);
            }

            

        }
    }
}