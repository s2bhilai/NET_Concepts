using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherData
{
    public class PubContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }

        public PubContext()
        {

        }

        public PubContext(DbContextOptions<PubContext> options)
            :base(options)
        {

        }

        //Comment this method if start up project is PubApp
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog=PubDatabase")
                    .LogTo(Console.WriteLine, new[]
                    { DbLoggerCategory.Database.Command.Name},
                    Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId=1,FirstName="Sai",LastName="Subin" });

            var authorList = new Author[]
            {
                new Author { AuthorId=2,FirstName="Omi",LastName="Subin" },
                new Author { AuthorId=3,FirstName="Chitra",LastName="Subin" },
                new Author { AuthorId=4,FirstName="Nibin",LastName="Subin" },
                new Author { AuthorId=5,FirstName="Sheela",LastName="Sadanandan" },
                new Author { AuthorId=6,FirstName="VK",LastName="Sadanandan" }
            };

            modelBuilder.Entity<Author>().HasData(authorList);

            var someBooks = new Book[]
            {
                new Book {BookId = 1 , AuthorId = 1,Title="Power of Subconscious mind",
                    PublishDate=new DateTime(1989,3,1)},
                new Book {BookId = 2 , AuthorId = 2,Title="Psychology of Money",
                    PublishDate=new DateTime(2013,12,11)},
                new Book {BookId = 3 , AuthorId = 3,Title="ReWork",
                    PublishDate=new DateTime(2020,3,1)}
            };

            modelBuilder.Entity<Book>().HasData(someBooks);

            var someArtists = new Artist[]
            {
                new Artist { ArtistId = 1, FirstName = "Christy",LastName = "Rajan"},
                new Artist { ArtistId = 2, FirstName = "Sanchit",LastName = "Saxena"},
                new Artist { ArtistId = 3, FirstName = "Bryan",LastName = "Adams"}
            };

            modelBuilder.Entity<Artist>().HasData(someArtists);

            var someCovers = new Cover[]
            {
                new Cover {CoverId = 1, DesignIdeas="Design 1",DigitalOnly = false, BookId = 3},
                new Cover {CoverId = 2, DesignIdeas="Design 2",DigitalOnly = true, BookId = 2},
                new Cover {CoverId = 3, DesignIdeas="Design 3",DigitalOnly = false, BookId = 1}
            };

            modelBuilder.Entity<Cover>().HasData(someCovers);

        }

        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries().ToList();
            var firstEntry = entities.First().Entity;

            return base.SaveChanges();
        }
    }
}
