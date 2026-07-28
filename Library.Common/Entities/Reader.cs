using Library.Common.Enums;
using Library.Common.Interfaces.DAL.Complex;

namespace Library.Common.Entities
{
    public class Reader : User, IReader
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public DocumentType DocumentType { get; set; }
    }
}