using Library.Common.Enums;

namespace Library.Common.Interfaces.DAL
{
    public interface IDocument : IID
    {
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
