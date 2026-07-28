using Library.Common.Enums;
using Library.Common.Interfaces.DAL;
using Library.Common.Interfaces.DAL.Complex;

namespace Library.Common.Entities
{
    public class Book : IBook
    {
        public Guid Id { get; set; }
        public string Name { get; set;  } = null!;
        public Genre Genre { get; set; }
        public int Count { get; set; }
        public Guid PublisherTypeId { get; set; }
        public PublisherCodeType PublisherType { get; set; } = null!;
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public DateTime PublishYear { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int ReturnedDays { get; set; } = 30;
        IPublisherCodeType IBook.PublisherType
        {
            get => PublisherType;
            set => PublisherType = (PublisherCodeType)value;
        }
        IEnumerable<IAuthor> IBook.Authors => Authors;
    }
}
