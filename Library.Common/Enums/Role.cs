namespace Library.Common.Enums
{
    [Flags]
    public enum Role
    {
        None = 0,
        Reader = 1,
        Librarian = 2,
        Admin = 4,
    }
}
