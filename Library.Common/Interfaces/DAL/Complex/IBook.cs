using Library.Common.Enums;

namespace Library.Common.Interfaces.DAL.Complex
{
    public interface IBook : IName
    {
        public int Count { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<IAuthor> Authors { get; }
        public Guid PublisherTypeId { get; set; }
        public IPublisherCodeType PublisherType { get; set; }
        public DateTime PublishYear { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
