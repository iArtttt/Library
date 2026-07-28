namespace Library.Common.Interfaces.DAL.Complex
{
    public interface IReader : IUser, IPerson, IDocument
    {
        public DateTime Birthday { get; set; }
    }
}
