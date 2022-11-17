// See https://aka.ms/new-console-template for more information


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PublisherData;
using PublisherDomain;

//using (PubContext context = new PubContext())
//{
//    context.Database.EnsureCreated();
//}

//AddAuthor();
//GetAuthors();
//AddAuthorWithBook();
//GetAuthorsWithBooks();
//QueryFilters();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
//SortAuthors();
//QueryAggregate();
//InsertAuthor();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthors();
//CordinatedRetrieveAndUpdateAuthor();
//InsertNewAuthorWithNewBook();
//AddNewBookToExistingAuthorInMemory();
//AddNewBookToExistingAuthorInMemoryViaBook();
//EagerLoadBooksWithAuthors();

//ExplicitLoadCollection();

//ModifyRelatedDataWhenTracked();

//ModifyRelatedDataWhenNOTTracked();

//CacsadeDeleteInActionWhenTracked();

//ConnectExistingArtistAndCoverObjects();

//RetrieveAnArtistWithTheirCovers();

//RetrieveAllArtistsWithTheirCover();

//RetrieveAllArtistsWhoHaveCovers();


//ReAssignCover();

//ONE To ONE//////////////////////////
//GetAllBooksWithTheirCovers();
//NewBookAndCover();

//AddCoverToExistingBook();
//ProtectingFromUniqueFK();

/// /////////////////////////////
/// 

///////RAW SQL///////////////////

SimpleRawSQL();




void CancelBookWithCustomTransaction(int bookId)
{
    using var context = new PubContext();
    using var transaction = context.Database.BeginTransaction();

    try
    {
        var artists = context.Artists
            .Where(a => a.Covers.Any(cover => cover.BookId == bookId)).ToList();

        context.Database.ExecuteSqlInterpolated($"Delete from books where bookId={bookId}");

        context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception)
    {

    }
}

void FormattedRawSQL_Safe()
{
    using var context = new PubContext();
    var lastNameStart = "L";
    var authors = context.Authors
        .FromSqlRaw("select * from authors where lastname like '{0}%' ", 
        lastNameStart).OrderBy(a => a.LastName).ToList();
    //Creates Parametrized queries

}

void SimpleRawSQL()
{
    using var context = new PubContext();
    var authors = context.Authors.FromSqlRaw("select * from authors").ToList();
}

void ProtectingFromUniqueFK()
{
    using var context = new PubContext();
    var TheNeverDesignIdeas = "A spirally spiral";
    var book = context.Books.Include(b => b.Cover)
        .FirstOrDefault(b => b.BookId == 5);

    if (book.Cover != null)
        book.Cover.DesignIdeas = TheNeverDesignIdeas;
    else
        book.Cover = new Cover { DesignIdeas = "A spirally spiral" };

    context.SaveChanges();
}

void AddCoverToExistingBook()
{
    using var context = new PubContext();
    var book = context.Books.Find(2);
    book.Cover = new Cover { DesignIdeas = "Some Cover" };

    context.SaveChanges();
}


void NewBookAndCover()
{
    using var context = new PubContext();
    var book = new Book { AuthorId = 1, Title = "The Brain", 
        PublishDate = new DateTime(1973, 1, 1) };

    book.Cover = new Cover { DesignIdeas = "The Brain Book" };
    context.Books.Add(book);
    context.SaveChanges();
}

void GetAllBooksWithTheirCovers()
{
    using var context = new PubContext();

    var booksandcovers = context.Books.Include(b => b.Cover).ToList();
    booksandcovers.ForEach(book =>
      Console.WriteLine(book.Title + 
         (book.Cover == null ? ":No cover yet" : ":" + book.Cover.DesignIdeas)));
}


void ReAssignCover()
{
    using var context = new PubContext();

    var coverWithArtist1 = context.Covers
        .Include(c => c.Artists.Where(a => a.ArtistId == 1))
        .FirstOrDefault(c => c.CoverId == 1);

    coverWithArtist1.Artists.RemoveAt(0);

    var artist3 = context.Artists.Find(3);
    coverWithArtist1.Artists.Add(artist3);

    context.ChangeTracker.DetectChanges();
    var debugView = context.ChangeTracker.DebugView.ShortView;
}



//UnAssignAnArtistFromCover();

//To Delete an Object That is joined to another
// Cascade delete to rescue
// If the join is being tracked, EF Core will cascade delete the join
// If the relationship is not being tracked, database cascade delete will
// remove the join

void UnAssignAnArtistFromCover()
{
    using var context = new PubContext();
    var coverwithanArtist = context.Covers
        .Include(c => c.Artists.Where(a => a.ArtistId == 1))
        .FirstOrDefault(c => c.CoverId == 1);

    coverwithanArtist.Artists.RemoveAt(0);

    context.ChangeTracker.DetectChanges();

    var debugView = context.ChangeTracker.DebugView.ShortView;
}

void RetrieveAllArtistsWhoHaveCovers()
{
    using var context = new PubContext();

    var artistWithCovers = context.Artists.Where(a => a.Covers.Any())
        .ToList();
}

void RetrieveAllArtistsWithTheirCover()
{
    using var context = new PubContext();
    var artistWithCovers = context.Artists.Include(a => a.Covers).ToList();
}


void RetrieveAnArtistWithTheirCovers()
{
    using var context = new PubContext();

    var artistWithCover = context.Artists.Include(a => a.Covers)
        .FirstOrDefault(a => a.ArtistId == 1);

}

void ConnectExistingArtistAndCoverObjects()
{
    using var context = new PubContext();

    var artistA = context.Artists.Find(1);
    var artistB = context.Artists.Find(2);
    var coverA = context.Covers.Find(1);

    coverA.Artists.Add(artistA);
    coverA.Artists.Add(artistB);

    context.SaveChanges();

    //Create New Cover with existing artist
    var artista1 = context.Artists.Find(1);
    var cover = new Cover { DesignIdeas = "Author has provided photo " };
    cover.Artists.Add(artista1);

    context.Covers.Add(cover);
    context.SaveChanges();

    //Creating new artist and cover together
    var newArtist = new Artist { FirstName = "Thumbi", LastName = "Prasad" };
    var newCover = new Cover { DesignIdeas = "We like birds!" };

    newArtist.Covers.Add(newCover);

    context.Artists.Add(newArtist);
    context.SaveChanges();



}

void CacsadeDeleteInActionWhenTracked()
{
    using var context = new PubContext();

    var author = context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 2);

    context.Authors.Remove(author);

    var state = context.ChangeTracker.DebugView.ShortView;
}

void ModifyRelatedDataWhenNOTTracked()
{
    using var context = new PubContext();

    var author = context.Authors.Include(a => a.Books)
       .FirstOrDefault(a => a.AuthorId == 2);

    author.Books[0].BasePrice = (decimal)12.0;

    var newContext = new PubContext(); //disconnected scenario
    //Update method updates all objects in graph
    //if the objects in graph have no key value, then those are Added
    //newContext.Books.Update(author.Books[0]);

    //This updates only Book Entity
    //DbContext.Entry gives a lot of fine grained control over
    //change tracker
    newContext.Entry(author.Books[0]).State = EntityState.Modified;
    var state = newContext.ChangeTracker.DebugView.ShortView;

}

void ModifyRelatedDataWhenTracked()
{
    using var context = new PubContext();

    var author = context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 2);

    author.Books.Remove(author.Books[0]);
    context.ChangeTracker.DetectChanges();

    var state = context.ChangeTracker.DebugView.ShortView;
}

void FilterUsingRelatedData()
{
    using var context = new PubContext();

    var recentAuthors = context.Authors
        .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
        .ToList();
    //Loads only Authors, no books
    // sql generates where exists clause
}


void ExplicitLoadCollection()
{
    using var context = new PubContext();

    var author = context.Authors.FirstOrDefault(a => a.LastName == "Subin");
    context.Entry(author).Collection(a => a.Books).Load();
}



void Projections()
{
    using var context = new PubContext();

    var unknownTypes = context.Authors
        .Select(a => new
        {
            AuthorId = a.AuthorId,
            Name = a.FirstName.First() + "" + a.LastName,
            Books = a.Books.Where(b => b.PublishDate.Year < 2000).Count()
        })
        .ToList();
}

void EagerLoadBooksWithAuthors()
{
    using var context = new PubContext();
    //var authors = context.Authors.Include(i => i.Books).ToList(); //LEFT JOIN

    var pubStart = new DateTime(2010, 1, 1);
    var authors = context.Authors
        .Include(a => a.Books
                        .Where(b => b.PublishDate >= pubStart)
                        .OrderBy(b => b.Title))
        .ToList();

    authors.ForEach(a =>
    {
        Console.WriteLine($"{a.LastName} ({a.Books.Count})");
        a.Books.ForEach(b => Console.WriteLine("  " + b.Title));
    });
}

void AddNewBookToExistingAuthorInMemoryViaBook()
{
    using var context = new PubContext();
    var book = new Book
    {
        Title = "Shift",
        PublishDate = new DateTime(2012, 1, 1)
    };
    book.Author = context.Authors.Find(7);
    context.Books.Add(book);
    //Author Entity is Unchanged, EF Core forced author state to Added when we add
    // via Authors DbSet, but not when adding from Books.
    context.SaveChanges();
}

void AddNewBookToExistingAuthorInMemory()
{
    using var context = new PubContext();
    var author = context.Authors.FirstOrDefault(a => a.LastName == "Muhammed");
    if(author != null)
    {
        author.Books.Add(
            new Book { Title = "Wool", PublishDate = new DateTime(2012, 1, 1) });

        //Don't Add, Passing a preexisting entity into its DbSet Add
        //will cause the EFCore
        //to try to insert into database.
        //context.Authors.Add(author);
    }

    context.SaveChanges();
}

void InsertNewAuthorWithNewBook()
{
    using var context = new PubContext();
    var author = new Author { FirstName = "Shareef", LastName = "Muhammed" };
    author.Books.Add(new Book
    {
        Title = "West with Giraffes",
        PublishDate = new DateTime(2021,2,1)
    });

    context.Authors.Add(author);

    context.SaveChanges();

}

void InsertMultipleAuthors()
{
    using var context = new PubContext();

    context.Authors.AddRange(new Author { FirstName = "Alia", LastName = "Shareef" },
        new Author { FirstName = "Joanna", LastName = "Sajo" },
        new Author { FirstName = "Immanueal", LastName = "Sajo" });

    context.SaveChanges();
}

void DeleteAnAuthor()
{
    using var context = new PubContext();

    var extraa = context.Authors.Find(25);
    if(extraa != null)
    {
        context.Authors.Remove(extraa);
        context.SaveChanges();
    }
}

void CordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(5);
    if(author?.FirstName == "Sai")
    {
        author.FirstName = "Samarth";
        SaveThatAuthor(author);
    }
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}

void SaveThatAuthor(Author author)
{
    using var shortLivedContext = new PubContext();
    shortLivedContext.Authors.Update(author);
    shortLivedContext.SaveChanges();

}

void RetrieveAndUpdateMultipleAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.Where(a => a.LastName == "Subin").ToList();

    //If 3 rows, then 3 update statements sent to DB
    foreach (var la in authors)
    {
        la.LastName = "SubinNibin";
    }

    Console.WriteLine("Before:" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("After:" + context.ChangeTracker.DebugView.ShortView);
    
    //When you call SaveCHanges, context will take one last look at the objects its
    //tracking
    context.SaveChanges();
}

void RetrieveAndUpdateAuthor()
{
    using var context = new PubContext();
    var author = context.Authors.FirstOrDefault(a => a.FirstName == "Sai" &&
        a.LastName == "Subin");

    if(author != null)
    {
        author.LastName = "Suchit";
        context.SaveChanges(); // In case of update, returns the no. of rows affected.,
        //Also sets the state to modified.
    }
}

void InsertAuthor()
{
    using var context = new PubContext();
    var author = new Author { FirstName = "Shareef", LastName = "Hussein" };
    context.Authors.Add(author);
    context.SaveChanges();
}

void QueryAggregate()
{
    using var context = new PubContext();
    //var auth = context.Authors.OrderBy(o => o.LastName)
    //    .LastOrDefault(a => a.LastName == "Subin");

    var author = context.Authors.OrderByDescending(a => a.FirstName)
        .FirstOrDefault(a => a.LastName == "Subin");


}


void SortAuthors()
{
    using var context = new PubContext();
    var authorsByLastName = context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();

    authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));

    var authorsDescending = context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();

    authorsDescending.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));
}

void AddSomeMoreAuthors()
{
    using var context = new PubContext();
    context.Authors.Add(new Author { FirstName = "Sai", LastName = "Subin" });
    context.Authors.Add(new Author { FirstName = "Omi", LastName = "Chitra" });
    context.Authors.Add(new Author { FirstName = "Samarth", LastName = "Subin" });
    context.Authors.Add(new Author { FirstName = "Adhyaan", LastName = "Suchit" });

    context.SaveChanges();
}

void SkipAndTakeAuthors()
{
    using var context = new PubContext();
    var groupSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}");
        foreach (var author in authors)
        {
            Console.WriteLine($" {author.FirstName} {author.LastName}");
        }
    }
}

void QueryFilters()
{
    using var context = new PubContext();
    var filter = "S%";
    var authors = context.Authors
        .Where(a => EF.Functions.Like(a.LastName,filter)).ToList();
}

void AddAuthor()
{
    var author = new Author { FirstName = "Nibin", LastName = "Sadanandan" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "Chitra", LastName = "Suresh" };
    author.Books.Add(
        new Book { Title = "Eat Healthy", PublishDate = new DateTime(2009, 1, 1) });
    author.Books.Add(
        new Book { Title = "Metallurgy", PublishDate = new DateTime(2010, 1, 1) });

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine("*" + book.Title);
        }
    }
}