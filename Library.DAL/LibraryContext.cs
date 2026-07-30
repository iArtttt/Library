using Library.Common.Entities;
using Library.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Library.DAL
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<PublisherCodeType> PublishingCodeTypes { get; set; } = null!;
        public DbSet<BorrowedBook> BorrowedBooks { get; set; } = null!;
        public LibraryContext(DbContextOptions<LibraryContext> optionsBuilder)
            : base(optionsBuilder)
        {
            //Database.EnsureCreated();
        }
        public LibraryContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDB_v2;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var codeType1 = new PublisherCodeType { Id = Guid.Parse("4F881976-6C99-469E-AB6D-8BB9ADE69D15"), PublisherCode = "ISBN" };
            var codeType2 = new PublisherCodeType { Id = Guid.Parse("E93A6306-87C8-43F1-9B98-018E4428561C"), PublisherCode = "ISSN" };
            var codeType3 = new PublisherCodeType { Id = Guid.Parse("19CCC910-4FCF-413E-85EC-AE8803F8788D"), PublisherCode = "ISRC" };
            var codeType4 = new PublisherCodeType { Id = Guid.Parse("8590AA52-7CBE-4D1C-9B10-ED4F8637957E"), PublisherCode = "ISWC" };
            
            modelBuilder.Entity<PublisherCodeType>().HasData(codeType1, codeType2, codeType3, codeType4);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            string adminSalt = "A1B2C3D4E5F67890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890";
            string readerSalt = "F9E8D7C6B5A43210FEDCBA0987654321FEDCBA0987654321FEDCBA0987654321";

            string GetSeedHash(string password, string salt)
            {
                using var sha256 = SHA256.Create();
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToHexString(bytes);
            }

            string adminHash = GetSeedHash("admin123", adminSalt);
            string admin1Hash = GetSeedHash("1234", adminSalt);
            string readerHash = GetSeedHash("reader123", readerSalt);
            string reader1Hash = GetSeedHash("4321", readerSalt);



            modelBuilder.Entity<User>()
                .HasData(
                new User() 
                { 
                    Id = Guid.Parse("010A3FC9-D742-41D3-BECD-F4F2669FC2C3"), 
                    Login = "Admin", 
                    PasswordHash = adminHash,
                    PasswordSalt = adminSalt,
                    Email = "admin@gmail.com",
                    Name = "Artur",
                    LastName = "Svichkar",
                    Role = Role.Librarian
                },
                new User() 
                { 
                    Id = Guid.Parse("575BAD15-19AA-4616-90D7-718006DCE32C"), 
                    Login = "Admin1", 
                    PasswordHash = admin1Hash,
                    PasswordSalt = adminSalt,
                    Email = "admin1@gmail.com",
                    Name = "Rick",
                    LastName = "Sunches",
                    Role = Role.Librarian
                },
                new User()
                {
                    Id = Guid.Parse("7D74A99A-BD3D-42E7-A461-9CC65BC26626"),
                    Login = "Reader",
                    PasswordHash = readerHash,
                    PasswordSalt = readerSalt,
                    Email = "reader@gmail.com",
                    Name = "Bob",
                    LastName = "Lighter",
                    Birthday = DateTime.Today,
                    DocumentType = DocumentType.DrivingLicence,
                    DocumentNumber = "3354213",
                    Role = Role.Reader
                },
                new User()
                {
                   Id = Guid.Parse("670EC28C-274B-4009-8F5D-637206220341"),
                    Login = "Reader1",
                    PasswordHash = reader1Hash,
                    PasswordSalt = readerSalt,
                    Email = "rEAr@gmail.com",
                    Name = "Alex",
                    LastName = "Zeroph",
                    Birthday = DateTime.Parse("18.04.1993"),
                    DocumentType = DocumentType.Passport,
                    DocumentNumber = "777789",
                    Role = Role.Reader
                }
                );


            var autor1 = new Author { Id = Guid.Parse("10CE119E-0CF5-478F-A016-964530F3C330"), Name = "Oleg", LastName = "Fiom" };
            var autor2 = new Author { Id = Guid.Parse("1F2F09F8-5326-4F16-81A2-81705A3406EA"), Name = "Ivan", LastName = "Syropin", SecondName = "Grozniy" };
            var autor3 = new Author { Id = Guid.Parse("A36DED85-BF17-4815-ADF1-CA2F07B81930"), Name = "Vasiliy", LastName = "Syropin", SecondName = "Krot" };

            modelBuilder.Entity<Author>().HasData(autor1, autor2, autor3);



            var book1 = new Book 
            { 
                Id = Guid.Parse("F527F881-937F-42BF-89B1-02DF6C19E8CD"), 
                Name = "C# for smart", 
                Genre = Genre.Learning, 
                Country = "Ukrain", 
                City = "Kharkiv", 
                PublisherTypeId = codeType1.Id, 
                Count = 2 
            };
            var book2 = new Book 
            { 
                Id = Guid.Parse("EB886598-4B81-4E2F-B11C-BA4B32FA5ED0"), 
                Name = "World Story", 
                Genre = Genre.History, 
                Country = "Ukrain", 
                City = "Kiev", 
                PublisherTypeId = codeType2.Id, 
                Count = 1 
            };
            var book3 = new Book 
            { 
                Id = Guid.Parse("5EBC58C4-2618-4348-B3EA-BF9BBC5F3A03"), 
                Name = "Summer Time", 
                Genre = Genre.Novel, 
                Country = "Ukrain", 
                City = "Kiev", 
                PublisherTypeId = codeType3.Id, 
                Count = 5 
            };
            var book4 = new Book 
            { 
                Id = Guid.Parse("AC61B44E-C484-4566-BFC0-13F6804B9C59"), 
                Name = "Mgla", 
                Genre = Genre.Horror,
                Country = "Poland", 
                PublisherTypeId = codeType1.Id, 
                Count = 2 
            };

            modelBuilder.Entity<Book>().HasData(book1, book2, book3, book4);

            modelBuilder.Entity("AuthorBook")
                .HasData(
                    new { AuthorsId = autor1.Id, BooksId = book1.Id },
                    new { AuthorsId = autor1.Id, BooksId = book2.Id },
                    new { AuthorsId = autor2.Id, BooksId = book2.Id },
                    new { AuthorsId = autor3.Id, BooksId = book3.Id },
                    new { AuthorsId = autor3.Id, BooksId = book4.Id }
                );



            base.OnModelCreating(modelBuilder);
        }
    }
}
