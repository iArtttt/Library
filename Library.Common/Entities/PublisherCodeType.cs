using Library.Common.Interfaces.DAL;

namespace Library.Common.Entities
{
    public class PublisherCodeType : IPublisherCodeType
    {
        public Guid Id { get; set; }
        public string PublisherCode { get; set; } = null!;
    }
}
